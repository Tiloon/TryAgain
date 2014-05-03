<?php
$server_list = json_decode(file_get_contents("./svlist.json", true));

foreach($server as $server_list) {
	echo($server + "\n");
}
?>