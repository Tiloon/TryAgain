if(target.UID != user.UID) {
	if(!(item.hasOwnProperty('refresh'))) {
		item["refresh"] = -1;
	}
	var time = (new Date()).getSeconds();
	if(item.refresh - time != 0) {
		target.stats.lp -= user.stats.force*5;
	}
	item.refresh = time;
}