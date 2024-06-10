<?php
echo "conexion Exitosa";
$servidor = 'fdb1034.awardspace.net';
$usuario = '4472624_zombie';
$pass = 'EW*bpIYg84D+unIs';
$baseDatos = '4472624_zombie';

// Intentar conectar al servidor MySQL
$conexion = new mysqli($servidor, $usuario, $pass, $baseDatos);

// Verificar si hay algún error de conexión
if ($conexion->connect_error) {
    die("Error de conexión: " . $conexion->connect_error);
} else {
    echo "Conexión exitosa";
}

// Cerrar la conexión
?>