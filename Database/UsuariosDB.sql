---- Database: UsuariosDB

--DROP DATABASE IF EXISTS "UsuariosDB";

--CREATE DATABASE "UsuariosDB"
--    WITH
--    OWNER = postgres
--    ENCODING = 'UTF8'
--    LC_COLLATE = 'Spanish_Colombia.1252'
--    LC_CTYPE = 'Spanish_Colombia.1252'
--    LOCALE_PROVIDER = 'libc'
--    TABLESPACE = pg_default
--    CONNECTION LIMIT = -1
--    IS_TEMPLATE = False;


---- Tabla Pais

--	CREATE TABLE pais 
--	(
--    pais_id SERIAL PRIMARY KEY,
--    nombre VARCHAR(150) NOT NULL
--	);

--	--insert tabla pais

--	INSERT INTO pais (nombre) VALUES('Colombia');


---- Tabla departamento
--	CREATE TABLE departamento (
--    departamento_id SERIAL PRIMARY KEY,
--    pais_id INT NOT NULL REFERENCES pais(pais_id),
--    nombre VARCHAR(150) NOT NULL
--);

----insert tabla departamento
--INSERT INTO departamento (pais_id, nombre) VALUES
--(1, 'Amazonas'),
--(1, 'Antioquia'),
--(1, 'Arauca'),
--(1, 'Atlántico'),
--(1, 'Bolívar'),
--(1, 'Boyacá'),
--(1, 'Caldas'),
--(1, 'Caquetá'),
--(1, 'Casanare'),
--(1, 'Cauca'),
--(1, 'Cesar'),
--(1, 'Chocó'),
--(1, 'Córdoba'),
--(1, 'Cundinamarca'),
--(1, 'Guainía'),
--(1, 'Guaviare'),
--(1, 'Huila'),
--(1, 'La Guajira'),
--(1, 'Magdalena'),
--(1, 'Meta'),
--(1, 'Nariño'),
--(1, 'Norte de Santander'),
--(1, 'Putumayo'),
--(1, 'Quindío'),
--(1, 'Risaralda'),
--(1, 'San Andrés y Providencia'),
--(1, 'Santander'),
--(1, 'Sucre'),
--(1, 'Tolima'),
--(1, 'Valle del Cauca'),
--(1, 'Vaupés'),
--(1, 'Vichada');


---- Tabla municipio
--CREATE TABLE municipio (
--    municipio_id SERIAL PRIMARY KEY,
--    departamento_id INT NOT NULL REFERENCES departamento(departamento_id),
--    nombre VARCHAR(150) NOT NULL
--);

----insert tabla municipio 

--INSERT INTO municipio (departamento_id, nombre) VALUES
--(2, 'Medellín'),
--(2, 'Bello'),
--(2, 'Envigado'),
--(2, 'Itagüí'),
--(2, 'Sabaneta'),
--(2, 'Rionegro');

--INSERT INTO municipio (departamento_id, nombre) VALUES
--(14, 'Bogotá'),
--(14, 'Soacha'),
--(14, 'Chía'),
--(14, 'Zipaquirá');

--INSERT INTO municipio (departamento_id, nombre) VALUES
--(4, 'Barranquilla'),
--(4, 'Soledad'),
--(4, 'Malambo'),
--(4, 'Galapa');

----select municipio
--select * from municipio


---- Tabla usuarios
--CREATE TABLE usuarios (
--    usuario_id SERIAL PRIMARY KEY,
--    nombre VARCHAR(200) NOT NULL,
--    telefono VARCHAR(50) UNIQUE NOT NULL,
--    direccion VARCHAR(300) NOT NULL,
--    pais_id INT NOT NULL REFERENCES pais(pais_id),
--    departamento_id INT NOT NULL REFERENCES departamento(departamento_id),
--    municipio_id INT NOT NULL REFERENCES municipio(municipio_id)
--);

---- select usuarios

--select * from usuarios

--select * from sp_obtener_todos_usuarios()

----sp crear usuario

--CREATE OR REPLACE FUNCTION sp_crear_usuario(
--    p_nombre VARCHAR,
--    p_telefono VARCHAR,
--    p_direccion VARCHAR,
--    p_pais_id INT,
--    p_departamento_id INT,
--    p_municipio_id INT
--)
--RETURNS INT AS $$
--DECLARE 
--    new_id INT;
--BEGIN
--    INSERT INTO usuarios(nombre, telefono, direccion, pais_id, departamento_id, municipio_id)
--    VALUES (p_nombre, p_telefono, p_direccion, p_pais_id, p_departamento_id, p_municipio_id)
--    RETURNING usuario_id INTO new_id;

--    RETURN new_id;
--END;
--$$ LANGUAGE plpgsql;


-----sp Obtener usuario por ID
--DROP FUNCTION IF EXISTS sp_obtener_usuario(INT);

--CREATE OR REPLACE FUNCTION sp_obtener_usuario(p_id INT)
--RETURNS TABLE (
--    usuario_id INT,
--    nombre VARCHAR,
--    telefono VARCHAR,
--    direccion VARCHAR,
--    pais VARCHAR,
--    departamento VARCHAR,
--    municipio VARCHAR
--) AS $$
--BEGIN
--    RETURN QUERY
--    SELECT 
--        u.usuario_id,
--        u.nombre,
--        u.telefono,
--        u.direccion,
--        p.nombre AS pais,
--        d.nombre AS departamento,
--        m.nombre AS municipio
--    FROM usuarios u
--    INNER JOIN pais p ON u.pais_id = p.pais_id
--    INNER JOIN departamento d ON u.departamento_id = d.departamento_id
--    INNER JOIN municipio m ON u.municipio_id = m.municipio_id
--    WHERE u.usuario_id = p_id;
--END;
--$$ LANGUAGE plpgsql;

----select sp_obtener_usuario
--select * from sp_obtener_usuario(2);
--select * from usuarios;
---- sp obtener todos
--DROP FUNCTION IF EXISTS sp_obtener_todos_usuarios();
--CREATE OR replace  FUNCTION sp_obtener_todos_usuarios()
--RETURNS TABLE (
--    usuario_id INT,
--    nombre VARCHAR,
--    telefono VARCHAR,
--    direccion VARCHAR,
--    pais VARCHAR,
--    departamento VARCHAR,
--    municipio VARCHAR
--) AS $$
--BEGIN
--    RETURN QUERY
--    SELECT 
--        u.usuario_id,
--        u.nombre,
--        u.telefono,
--        u.direccion,
--        p.nombre AS pais,
--        d.nombre AS departamento,
--        m.nombre AS municipio
--    FROM usuarios u
--    INNER JOIN pais p ON u.pais_id = p.pais_id
--    INNER JOIN departamento d ON u.departamento_id = d.departamento_id
--    INNER JOIN municipio m ON u.municipio_id = m.municipio_id
--    ORDER BY u.usuario_id;
--END;
--$$ LANGUAGE plpgsql;

---- select sp_obtener_todos_usuarios();

--select * from sp_obtener_todos_usuarios();

----sp actualizar usuario

--CREATE OR REPLACE FUNCTION sp_actualizar_usuario(
--    p_id INT,
--    p_nombre VARCHAR,
--    p_telefono VARCHAR,
--    p_direccion VARCHAR,
--    p_pais_id INT,
--    p_departamento_id INT,
--    p_municipio_id INT
--)
--RETURNS BOOLEAN AS $$
--BEGIN
--    UPDATE usuarios SET 
--        nombre = p_nombre,
--        telefono = p_telefono,
--        direccion = p_direccion,
--        pais_id = p_pais_id,
--        departamento_id = p_departamento_id,
--        municipio_id = p_municipio_id
--    WHERE usuario_id = p_id;

--    RETURN FOUND;
--END;
--$$ LANGUAGE plpgsql;

----sp eliminar usuario
--CREATE OR REPLACE FUNCTION sp_eliminar_usuario(p_id INT)
--RETURNS BOOLEAN AS $$
--BEGIN
--    DELETE FROM usuarios WHERE usuario_id = p_id;
--    RETURN FOUND;
--END;
--$$ LANGUAGE plpgsql;

