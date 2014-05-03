<?php
$server_list = json_decode(file_get_contents("./svlist.json", true));

$ip = 0;
if (!empty($_SERVER['HTTP_CLIENT_IP'])) {
    $ip = $_SERVER['HTTP_CLIENT_IP'];
} elseif (!empty($_SERVER['HTTP_X_FORWARDED_FOR'])) {
    $ip = $_SERVER['HTTP_X_FORWARDED_FOR'];
} else {
    $ip = $_SERVER['REMOTE_ADDR'];
}

array_push(&$server_list, $ip);

$server_list = array_unique($server_list);

file_put_contents("./svlist.json", json_encode($server_list));
?>