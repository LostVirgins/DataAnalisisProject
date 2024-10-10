<?php
// UsersInfo - player name, country, age, gender, joindate
// NewSessions - begin date, end date, playerID
// ItemSales - itemID, buy date sessionID
// Items - default



// Database config
$servername = 'localhost';
$dbname = 'franciscofp4';
$username = 'franciscofp4';
$password = '24559836e';

// Create a connection to the database
$conn = new mysqli($servername, $username, $password, $dbname);

$name = $_POST["playerName"];
$age = $_POST["age"];
$gender = $_POST["gender"];

$sql = "INSERT INTO UserInfo (`Name`, Age, Gender)
VALUES ('$name', '$age', '$gender')";

if($conn->connect_error) {
    die("Connection failed: " . $conn->connect_error);
}
else{
    if($conn->query($sql) === TRUE){
        $last_id = $conn->insert_id;
        echo "Newrecordcreated succesfully. Last insertedd ID is: " . $last_id;
    }
    else{
        die("Error: " . $sql . "<br>" . $conn->error);
    }
}

?>