drop database if exists PizzaArcade;
create database PizzaArcade;
use PizzaArcade;

CREATE TABLE Usuario(
    idUsuario INT NOT NULL AUTO_INCREMENT,
    usuario VARCHAR(50) NOT NULL,
    nombre VARCHAR(100) NOT NULL,
    apellido VARCHAR(100) NOT NULL,
    email VARCHAR(150) NOT NULL,
    passwordHash VARCHAR(255) NOT NULL,
    telefono VARCHAR(20),
    direccion VARCHAR(150),

    CONSTRAINT PK_Usuario PRIMARY KEY (idUsuario),
    CONSTRAINT UQ_Usuario_Email UNIQUE (email)
);

create table Cliente(
    idCliente int not null auto_increment,
    nombre varchar(100) not null,
    telefono varchar(20) not null,
    direccion varchar(150) not null,
    constraint PK_Cliente primary key (idCliente)
);


create table Pizza(
    idPizza int not null auto_increment,
    nombre varchar(100) not null,
    descripcion varchar(255),
    precio decimal(10,2) not null,
    disponible boolean not null,
    constraint PK_Pizza primary key (idPizza)
);


create table Pedido(
    idPedido int not null auto_increment,
    idCliente int not null,
    fechaHora datetime not null,
    estado int not null,
    direccionEntrega varchar(150) not null,
    total decimal(10,2) not null,
    constraint PK_Pedido primary key (idPedido),
    constraint FK_Pedido_Cliente foreign key (idCliente)
        references Cliente(idCliente)
);


create table DetallePedido(
    idPedido int not null,
    idPizza int not null,
    cantidad int not null,
    precioUnitario decimal(10,2) not null,
    constraint PK_DetallePedido primary key (idPedido, idPizza),
    constraint FK_DetallePedido_Pedido foreign key (idPedido)
        references Pedido(idPedido),
    constraint FK_DetallePedido_Pizza foreign key (idPizza)
        references Pizza(idPizza)
);