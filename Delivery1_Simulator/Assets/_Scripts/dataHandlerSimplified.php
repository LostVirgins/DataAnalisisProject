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

if($conn->connect_error) {
    die("Connection failed: " . $conn->connect_error);
}

$table = $_POST["table"];

switch ($table) {
    case "UserInfo":
        //Users
        $name = $_POST["playerName"];
        $country = $_POST["country"];
        $age = $_POST["age"];
        $gender = $_POST["gender"];
        $joindate = $_POST["joinDate"];

        $sql = "INSERT INTO UserInfo (`Name`, Age, Gender, Country, JoinDate)
                    VALUES ('$name', '$age', '$gender', '$country', '$joindate')";
        break;

    case "NewSessions":
        if (isset($_POST["beginSessionDate"]) && isset($_POST["playerId"])) {
            // Insert new session
            $beginDate = $_POST["beginSessionDate"];
            $playerID = $_POST["playerId"];

            $sql = "INSERT INTO NewSessions (`BeginDate`, `PlayerID`)
                    VALUES ('$beginDate', '$playerID')";
        } elseif (isset($_POST["endSessionDate"]) && isset($_POST["sessionId"])) {
            // Update session with end date
            $endDate = $_POST["endSessionDate"];
            $sessionID = $_POST["sessionId"];

            $sql = "UPDATE NewSessions SET `EndDate`='$endDate' WHERE `SessionID`='$sessionID'";
        } else {
            die("Error: Missing data for session handling");
        }
        break;

    case "ItemSales":
        //Sales
        $itemID = $_POST["itemId"];
        $buyDate = $_POST["buyDate"];
        $sessionID = $_POST["sessionId"];

        $sql = "INSERT INTO ItemSales (`ItemID`, `DateTime`, `SessionID`)
                VALUES ('$itemID', '$buyDate', '$sessionID')";
        break;
    
    default:
        die("Error: Invalid table name");
}

if($conn->query($sql) === TRUE){
    $last_id = $conn->insert_id;
    echo $last_id;
}
else{
    die("Error: " . $sql . "<br>" . $conn->error);
}

$conn->close();
?>