# UsersInfo - player name, country, age, gender, joindate
# NewSessions - begin date, end date, playerID
# ItemSales - itemID, buy date sessionID
# Items - default

<?php
// Database config
$host = 'localhost';
$dbname = 'database';
$username = 'username';
$password = 'password';

try {
    // Create a connection to the database
    $pdo = new PDO("mysql:host=$host;dbname=$dbname;charset=utf8", $username, $password);
    $pdo->setAttribute(PDO::ATTR_ERRMODE, PDO::ERRMODE_EXCEPTION);

    // Check if data was sent
    if ($_SERVER['REQUEST_METHOD'] === 'POST')
    {
        $playerName = isset($_POST['playerName']) ? $_POST['playerName'] : '';
        #$email = isset($_POST['email']) ? $_POST['email'] : '';

        echo $name;
        #echo $email;

        // Prepare SQL statement
        #$stmt = $pdo->prepare("INSERT INTO users (name, email) VALUES (:name, :email)");

        // Bind parameters
        #$stmt->bindParam(':name', $name);
        #$stmt->bindParam(':email', $email);

        // Execute the statement
        if ($stmt->execute()) {
            echo "User successfully added!";
        } else {
            echo "Failed to add user.";
        }
    } else {
        echo "No data received.";
    }
} catch (PDOException $e) {
    echo "Error: " . $e->getMessage();
}

// Close the connection
$pdo = null;
?>