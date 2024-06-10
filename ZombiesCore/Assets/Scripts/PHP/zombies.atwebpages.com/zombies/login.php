<?php
$servidor = 'fdb1034.awardspace.net';
$usuario = '4472624_zombie';
$pass = 'EW*bpIYg84D+unIs';
$baseDatos = '4472624_zombie';

// Conexión a la base de datos
$conexion = new mysqli($servidor, $usuario, $pass, $baseDatos);

if ($conexion->connect_error) {
    die("Error de conexión: " . $conexion->connect_error);
}
else{
    echo: "conectado";
}

// Obtener los datos del formulario de registro
$nombreUsuario = $_POST['nombre_usuario'];
$password=md5( $_POST['password']);
$edad = $_POST['edad'];

// Consulta SQL para insertar el nuevo usuario en la base de datos
$sql = "INSERT INTO Usuarios (Nombre, Password, Edad) VALUES ('$nombreUsuario', '$password', $edad)";

if ($conexion->query($sql) === TRUE) {
    echo "Usuario registrado exitosamente";
} else {
    echo "Error al registrar usuario: " . $conexion->error;
}

// Cerrar la conexión
$conexion->close();
?>
