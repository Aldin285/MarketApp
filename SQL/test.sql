-- Database: postgres

-- DROP DATABASE IF EXISTS postgres;

-- CREATE DATABASE postgres
--     WITH
--     OWNER = postgres
--     ENCODING = 'UTF8'
--     LC_COLLATE = 'French_France.1252'
--     LC_CTYPE = 'French_France.1252'
--     LOCALE_PROVIDER = 'libc'
--     TABLESPACE = pg_default
--     CONNECTION LIMIT = -1
--     IS_TEMPLATE = False;

-- COMMENT ON DATABASE postgres
--     IS 'default administrative connection database';


-- MarketApp supermarket database schema and test data.

DROP TABLE IF EXISTS order_items CASCADE;
DROP TABLE IF EXISTS orders CASCADE;
DROP TABLE IF EXISTS articles CASCADE;
DROP TABLE IF EXISTS categories CASCADE;
DROP TABLE IF EXISTS users CASCADE;

CREATE TABLE users (
    id bigint GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    name varchar(255) NOT NULL,
    email varchar(255) NOT NULL UNIQUE,
    type varchar(255) NOT NULL,
    created_at timestamptz NOT NULL DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE categories (
    id bigint GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    name varchar(100) NOT NULL UNIQUE,
    description varchar(500)
);

CREATE TABLE articles (
    id bigint GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    category_id bigint NOT NULL REFERENCES categories(id),
    barcode bigint(13) UNIQUE,
    title varchar(255) NOT NULL,
    description varchar(1000),
    price numeric(10, 2) NOT NULL CHECK (price >= 0),
    unit varchar(20) NOT NULL DEFAULT 'piece',
    stock_quantity integer NOT NULL DEFAULT 0 CHECK (stock_quantity >= 0),
    minimum_stock integer NOT NULL DEFAULT 0 CHECK (minimum_stock >= 0),
    is_active boolean NOT NULL DEFAULT true,
    created_at timestamptz NOT NULL DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE orders (
    id bigint GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    user_id bigint NOT NULL REFERENCES users(id),
    status varchar(20) NOT NULL DEFAULT 'pending'
        CHECK (status IN ('pending', 'paid', 'preparing', 'completed', 'cancelled')),
    ordered_at timestamptz NOT NULL DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE order_items (
    id bigint GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    order_id bigint NOT NULL REFERENCES orders(id) ON DELETE CASCADE,
    article_id bigint NOT NULL REFERENCES articles(id),
    quantity integer NOT NULL CHECK (quantity > 0),
    unit_price numeric(10, 2) NOT NULL CHECK (unit_price >= 0),
    UNIQUE (order_id, article_id)
);

-- Test users.
INSERT INTO users (name, email, type) VALUES
    ('Alice Martin', 'alice.martin@example.com', 'customer'),
    ('Benoit Durand', 'benoit.durand@example.com', 'employee'),
    ('Caroline Petit', 'caroline.petit@example.com', 'manager');

-- Supermarket categories.
INSERT INTO categories (name, description) VALUES
    ('Default', 'Aucune catégorie spécifique, utilisée par défaut pour les articles sans catégorie.'),
    ('Fruits et légumes', 'Produits frais : fruits, légumes et herbes'),
    ('Produits laitiers', 'Lait, yaourts, fromages et beurre'),
    ('Épicerie', 'Produits secs, conserves et boissons'),
    ('Boulangerie', 'Pain, viennoiseries et pâtisseries');

-- Supermarket articles.
INSERT INTO articles
    (category_id, barcode, title, description, price, unit, stock_quantity, minimum_stock)
VALUES
    ((SELECT id FROM categories WHERE name = 'Fruits et légumes'),
        '3760000000001', 'Pommes Gala', 'Pommes françaises en vrac', 2.49, 'Kg', 80, 10),
    ((SELECT id FROM categories WHERE name = 'Fruits et légumes'),
        '3760000000002', 'Tomates grappe', 'Tomates fraîches en barquette de 500 g', 2.19, 'Tray', 35, 8),
    ((SELECT id FROM categories WHERE name = 'Produits laitiers'),
        '3760000000003', 'Lait demi-écrémé', 'Bouteille de lait demi-écrémé UHT, 1 L', 1.15, 'Bottle', 60, 12),
    ((SELECT id FROM categories WHERE name = 'Produits laitiers'),
        '3760000000004', 'Emmental râpé', 'Sachet d''emmental râpé, 200 g', 2.95, 'Bag', 25, 5),
    ((SELECT id FROM categories WHERE name = 'Épicerie'),
        '3760000000005', 'Pâtes penne', 'Paquet de pâtes de blé dur, 500 g', 1.49, 'Pack', 100, 20),
    ((SELECT id FROM categories WHERE name = 'Épicerie'),
        '3760000000006', 'Eau minérale', 'Pack de six bouteilles de 1,5 L', 3.60, 'Pack', 40, 6),
    ((SELECT id FROM categories WHERE name = 'Boulangerie'),
        '3760000000007', 'Baguette tradition', 'Baguette tradition cuite sur place', 1.10, 'Piece', 30, 5);

-- Test order with its lines.
INSERT INTO orders (user_id, status)
VALUES ((SELECT id FROM users WHERE email = 'alice.martin@example.com'), 'paid');

INSERT INTO order_items (order_id, article_id, quantity, unit_price)
VALUES
    (
        (SELECT id FROM orders WHERE user_id = (SELECT id FROM users WHERE email = 'alice.martin@example.com') ORDER BY id DESC LIMIT 1),
        (SELECT id FROM articles WHERE barcode = '3760000000001'),
        2,
        (SELECT price FROM articles WHERE barcode = '3760000000001')
    ),
    (
        (SELECT id FROM orders WHERE user_id = (SELECT id FROM users WHERE email = 'alice.martin@example.com') ORDER BY id DESC LIMIT 1),
        (SELECT id FROM articles WHERE barcode = '3760000000005'),
        1,
        (SELECT price FROM articles WHERE barcode = '3760000000005')
    );
