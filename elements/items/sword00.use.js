if(!(item.hasOwnProperty('refresh'))) {
	item["refresh"] = -1;
}
item["coucou"] = -1;
var time = (new Date()).getSeconds();
if(item.refresh - time != 0) {
	target.stats.lp -= user.stats.force;
}
item.refresh = time;