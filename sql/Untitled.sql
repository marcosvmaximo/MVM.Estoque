use `meu-mysql`;

/* Lógico_2: */

CREATE TABLE Produto (
    Id CHAR(36) PRIMARY KEY,
    Categoria int,
    Nome varchar(100),
    Preco decimal(8,2),
    fk_Fornecedor_Id CHAR(36),
    fk_Fornecedor_Cpf varchar(11)
);

CREATE TABLE Fornecedor (
    Id CHAR(36),
    Cpf varchar(11),
    Nome varchar(100),
    Data_nascimento date,
    Logradouro varchar(100),
    Complemento varchar(100),
    Cep varchar(9),
    Bairro varchar(100),
    Cidade varchar(100),
    Uf varchar(2),
    Numero int,
    PRIMARY KEY (Id, Cpf),
    UNIQUE (Cpf, Id)
);
 
ALTER TABLE Produto ADD CONSTRAINT FK_Produto_2
    FOREIGN KEY (fk_Fornecedor_Id, fk_Fornecedor_Cpf)
    REFERENCES Fornecedor (Id, Cpf)
    ON DELETE CASCADE;