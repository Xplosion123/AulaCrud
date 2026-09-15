create database Oficina_db;
use Oficina_db;
CREATE TABLE contatos (
id INT AUTO_INCREMENT PRIMARY KEY,
nome VARCHAR(150) NOT NULL,
telefone VARCHAR(20) NOT NULL,
email VARCHAR(150) NOT NULL
);
DELIMITER $$
CREATE PROCEDURE sp_contato_criar (
IN p_nome VARCHAR(150),
IN p_tel VARCHAR(20),
IN p_email VARCHAR(150)
)
BEGIN
INSERT INTO contatos (nome, telefone, email)
VALUES (p_nome, p_tel, p_email);
END $$
CREATE PROCEDURE sp_contato_listar()
BEGIN
SELECT id, nome, telefone, email
FROM contatos
ORDER BY nome;
END $$
CREATE PROCEDURE sp_contato_editar (
IN p_id INT,
IN p_nome VARCHAR(150),
IN p_tel VARCHAR(20),
IN p_email VARCHAR(150)
)
BEGIN
UPDATE contatos
SET nome = p_nome,
telefone = p_tel,
email = p_email
WHERE id = p_id;
END $$
CREATE PROCEDURE sp_contato_excluir (
IN p_id INT
)
BEGIN
DELETE FROM contatos
WHERE id = p_id;
END $$
CREATE PROCEDURE sp_contato_obter (
IN p_id INT
)
BEGIN
SELECT id, nome, telefone, email
FROM contatos
WHERE id = p_id;
END $$
DELIMITER ;