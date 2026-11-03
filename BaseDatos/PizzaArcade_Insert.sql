USE PizzaArcade;

-- ==========================================
-- 1. CARGAR EL MENÚ DE PIZZAS
-- ==========================================
INSERT INTO Pizza (nombre, descripcion, precio, disponible) VALUES 
('Muzzarella', 'Salsa de tomate, abundante muzzarella y aceitunas verdes', 7500.00, true),
('Jamon y Queso', 'Muzzarella, jamón cocido, aceitunas y orégano. Un sabor clásico, simple y riquísimo.', 8800.00, true),
('Peperoni','Salsa de tomate, muzzarella y abundantes rodajas de pepperoni crocante. Un clásico americano con un toque picante.', 9200.00, true);

-- ==========================================
-- 2. CARGAR CLIENTES DE PRUEBA (Opcional pero útil)
-- ==========================================
INSERT INTO Cliente (nombre, telefono, direccion) VALUES 
('Goku', '11334455667', 'Av. Siempre Viva 742'),
('Sung Jin-woo', '76655443311', 'CABA');