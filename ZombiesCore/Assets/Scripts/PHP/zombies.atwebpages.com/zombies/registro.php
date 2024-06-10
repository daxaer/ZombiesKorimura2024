<?php
$servidor = 'fdb1034.awardspace.net';
$usuario = '4472624_zombie';
$pass = 'EW*bpIYg84D+unIs';
$baseDatos = '4472624_zombie';

// Conexión a la base de datos
$conexion = new mysqli($servidor, $usuario, $pass, $baseDatos);

// Verificar la conexión
if ($conexion->connect_error) {
    die("Error de conexión: " . $conexion->connect_error);
}
else{
    echo "conexion Exitosa";
}

// Obtener los datos del formulario de registro
$nombreUsuario = $_POST['nombre_usuario'];
$password = md5($_POST['password']); // Cifrar la contraseña
$edad = $_POST['edad'];

// Consulta SQL para verificar si el usuario ya existe
$sql_verificacion = "SELECT * FROM Usuarios WHERE Nombre = '$nombreUsuario'";
$resultado_verificacion = $conexion->query($sql_verificacion);

// Verificar si se encontró algún usuario con el mismo nombre
if ($resultado_verificacion->num_rows > 0) {
    // Enviar mensaje de error al cliente
    die("El nombre de usuario ya está en uso.");
} else {
    // Consulta SQL para insertar el nuevo usuario en la base de datos
    $sql_registro = "INSERT INTO Usuarios (Nombre, Password, Edad) VALUES ('$nombreUsuario', '$password', $edad)";

    if ($conexion->query($sql_registro) === TRUE) {
        // Enviar mensaje de éxito al cliente
        echo "Usuario registrado exitosamente";
    } else {
        // Enviar mensaje de error al cliente
        die("Error al registrar usuario: " . $conexion->error);
    }
}

// Cerrar la conexión
$conexion->close();
?>
