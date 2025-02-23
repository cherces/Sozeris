CREATE TABLE "JwtRefreshTokens" (
                            "Id" SERIAL PRIMARY KEY,
                            "UserId" INT NOT NULL,
                            "Token" VARCHAR(255) NOT NULL
);

ALTER TABLE "JwtRefreshTokens" OWNER TO postgres;

CREATE TABLE "Products" (
    "Id" SERIAL PRIMARY KEY,
    "Name" VARCHAR(255) NOT NULL,
    "Price" NUMERIC(10, 2) NOT NULL
);

ALTER TABLE "Products" OWNER TO postgres;

INSERT INTO "Products" ("Name", "Price") VALUES
('Белый хлеб', 25),
('Черный хлеб', 30);

CREATE TABLE "Users" (
    "Id" SERIAL PRIMARY KEY,
    "Login" VARCHAR(255) NOT NULL,
    "Password" VARCHAR(255) NOT NULL,
    "Role" INT NOT NULL,
    "Name" VARCHAR(255),
    "Surname" VARCHAR(255),
    "Phone" VARCHAR(50)
);

ALTER TABLE "Users" OWNER TO postgres;

INSERT INTO "Users" ("Login", "Password", "Role", "Name", "Surname", "Phone") VALUES
    ('admin', 'adminpassword', 3, 'Алексей', 'Сидоров', '89161234567'),
    ('user1', 'password1', 2, 'Иван', 'Иванов', '89169876543'),
    ('user2', 'password2', 1, 'Петр', 'Петров', '89162345678'),
    ('user3', 'password3', 1, 'Мария', 'Иванова', '89163456789'),
    ('user4', 'password4', 1, 'Анна', 'Кузнецова', '89164567890');

CREATE TABLE "Subscriptions" (
    "Id" SERIAL PRIMARY KEY,
    "UserId" INT NOT NULL,
    "StartDate" TIMESTAMP NOT NULL,
    "EndDate" TIMESTAMP NOT NULL,
    "IsActive" BOOLEAN NOT NULL,
    FOREIGN KEY ("UserId") REFERENCES "Users"("Id")
);

ALTER TABLE "Subscriptions" OWNER TO postgres;

INSERT INTO "Subscriptions" ("UserId", "StartDate", "EndDate", "IsActive") VALUES
    (1, '2023-01-01', '2024-01-01', TRUE),
    (2, '2023-01-10', '2024-01-10', TRUE),
    (3, '2023-01-15', '2024-01-15', FALSE),
    (4, '2023-02-01', '2024-02-01', TRUE),
    (5, '2023-02-10', '2024-02-10', TRUE);

CREATE TABLE "Orders" (
    "Id" SERIAL PRIMARY KEY,
    "SubscriptionId" INT NOT NULL,
    "ProductId" INT NOT NULL,
    "Quantity" INT NOT NULL,
    "Price" DECIMAL NOT NULL,
    FOREIGN KEY ("SubscriptionId") REFERENCES "Subscriptions"("Id"),
    FOREIGN KEY ("ProductId") REFERENCES "Products"("Id")
);

ALTER TABLE "Orders" OWNER TO postgres;

INSERT INTO "Orders" ("SubscriptionId", "ProductId", "Quantity", "Price") VALUES
    (1, 1, 2, 120),
    (1, 2, 1, 170),
    (2, 1, 3, 80),
    (3, 2, 4, 75),
    (4, 1, 2, 210),
    (5, 2, 3, 145);

CREATE TABLE "Deliveries" (
    "Id" SERIAL PRIMARY KEY,
    "SubscriptionId" INT NOT NULL,
    "DeliveryDate" TIMESTAMP NOT NULL,
    "IsDelivered" BOOLEAN NOT NULL,
    FOREIGN KEY ("SubscriptionId") REFERENCES "Subscriptions"("Id")
);

ALTER TABLE "Deliveries" OWNER TO postgres;

INSERT INTO "Deliveries" ("SubscriptionId", "DeliveryDate", "IsDelivered") VALUES
    (1, '2023-01-05', TRUE),
    (2, '2023-01-15', FALSE),
    (3, '2023-01-20', TRUE),
    (4, '2023-02-05', TRUE),
    (5, '2023-02-12', FALSE);