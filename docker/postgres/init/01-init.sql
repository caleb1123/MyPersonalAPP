-- extension để dùng gen_random_uuid
CREATE EXTENSION IF NOT EXISTS "pgcrypto";

-- tạo role
INSERT INTO roles (id, name)
VALUES
  (gen_random_uuid(), 'Admin'),
  (gen_random_uuid(), 'User');