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

//Users
$name = $_POST["playerName"];
$age = $_POST["age"];
$gender = $_POST["gender"];
$conuntry = $_POST["country"];
$joindate = $_POST["joinDate"];

//Sessions
$joindate = $_POST["joinDate"];
$joindate = $_POST["joinDate"];


$userSql = "INSERT INTO UserInfo (`Name`, Age, Gender, Country, JoinDate)
VALUES ('$name', '$age', '$gender', '$country', '$joindate')";

if($conn->connect_error) {
    die("Connection failed: " . $conn->connect_error);
}
else{
    if($conn->query($userSql) === TRUE){
        $last_id = $conn->insert_id;
        //echo "New record created succesfully. Last insertedd ID is: " . $last_id;
    }
    else{
        die("Error: " . $userSql . "<br>" . $conn->error);
    }

    if($conn->query($sql) === TRUE){
        $last_id = $conn->insert_id;
        //echo "New record created succesfully. Last insertedd ID is: " . $last_id;
    }
    else{
        die("Error: " . $userSql . "<br>" . $conn->error);
    }
}

?>