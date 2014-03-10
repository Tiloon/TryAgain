if((target.UID != user.UID) && (target.Type.split(',')[1] == "Character")) {
	if(!(item.hasOwnProperty('refresh'))) {
		item["refresh"] = {};
		item.refresh.s = -1;
		item.refresh.ms = -1;
	}
	//msg(JSON.stringify(item));
	var sec = (new Date()).getSeconds();
	var ms = (new Date()).getMilliseconds();
	
	if((item.refresh.s - sec != 0) || (item.refresh.ms - 500 > ms)){
		target.stats.lp -= user.stats.force*5;
	}
	item.refresh.s = sec;
	item.refresh.ms = ms;
}
