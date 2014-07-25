String.prototype.trim = function () { return this.replace(/^\s+|\s+$/g, ''); };
String.prototype.ltrim = function () { return this.replace(/^\s+/, ''); };
String.prototype.rtrim = function () { return this.replace(/\s+$/, ''); };
String.prototype.replaceAll = function (o, n) { var r = new RegExp(o, 'gim'); return this.replace(r, n); };
String.prototype.Format = function () { var content = this; for (var i = 0; i < arguments.length; i++) content = content.replace(new RegExp('\\{' + i + '\\}', 'gm'), arguments[i]); return content; };
var _d = document, _w = window, _n = navigator, _s = window.screen;
var _isIE = _n.appName.toLowerCase().indexOf('explorer') >= 0, _isSecure = _d.location.protocol == "https:";
var _rpdom = (_isSecure ? "https:" : "http:") + "//ad.reklamport.com/", _redurl = _rpdom + "rpclk2.ashx", _rsdom = (_isSecure ? "https:" : "http:") + "//res.reklamport.com/";
var _stickyid = "", rpClsSec = 8, rpgoto = null, rptrscs = 0;
if (typeof _templates == "undefined") { var _templates = []; }
setInterval("rptrscs++", 250);
var http = _d.location.protocol == 'https:' ? 'https://' : 'http://';
var runmode = { getad: 1, orfad: 2 };
var pageRunMode = runmode.getad, chkdUL = false, evcls = 0, evint = 0, rmtimer = 0, _rmcid = 0, _rmtid = 0, rmtimerid = 0;
var rpdom = http + 'ad.reklamport.com/';
var getad = rpdom + 'rpgetad.ashx?tt={0}{1}&rnd={2}', clkUrlFL = rpdom + 'rpclk2.ashx?c={0}%26t={1}%26URL={2}', clkUrl = rpdom + 'rpclk2.ashx?c={0}&t={1}&URL={2}';
_templates["img"] = "<a href='{0}?c={1}&t={2}&URL={3}' target='_blank' id='rpCont{1}.{2}' style='width:{5}px;height:{6}px;line-height:0px;display:table;'><img src='{4}' border=0 width='{5}' height='{6}' alt='TIKLAYIN'></a>";
_templates["swf"] = "<div style='position:relative;width:{1}px;height:{2}px;z-index:{6};' id='rpCont{4}.{5}'><object style='position:relative;padding:0;left:0px;top:0px;z-index:{6};' height='{2}px' width='{1}px' align='top' id='rpCont{4}.{5}Flash' codebase='http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=5,0,0,0'><param name='movie' value='{0}'><param name='quality' value='high'><param name='menu' value='false'><param name='wmode' value='{3}'><param name='allowscriptaccess' value='always'><param name='FlashVars' value='clickTAG={7}'><embed allowscriptaccess='always' id='rpCont{4}.{5}FlashEmbed' src='{0}' quality='high' menu='false' wmode='{3}' pluginspage='http://www.macromedia.com/shockwave/download/index.cgi?P1_Prod_Version=ShockwaveFlash' type='application/x-shockwave-flash' width='{1}' height='{2}'></embed></object></div>";
_templates["expandable"] = "<div style='position:relative;width:{1}px;height:{2}px;'><div id='rpCont{4}.{5}' style='position:absolute;overflow:hidden;width:{1}px;height:{2}px;z-index:{6};{7}'><object style='padding:0;z-index:{6};{7}' height='{2}px' width='{1}px' align='top' id='rpCont{4}.{5}Flash' codebase='http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=5,0,0,0'><param name='movie' value='{0}'><param name='quality' value='high'><param name='menu' value='false'><param name='wmode' value='{3}'><param name='allowscriptaccess' value='always'><param name='FlashVars' value='clickTAG={8}'><embed allowscriptaccess='always' id='rpCont{4}.{5}FlashEmbed' src='{0}' quality='high' menu='false' wmode='{3}' pluginspage='http://www.macromedia.com/shockwave/download/index.cgi?P1_Prod_Version=ShockwaveFlash' type='application/x-shockwave-flash' width='{1}' height='{2}'></embed></object></div></div>";
_templates["rollover"] = "<div id='rpCont{6}.{7}' onmouseout=this.style.width='{0}px';this.style.height='{1}px' onmouseover=this.style.width='{0}px';this.style.height='{3}px' style='width:{0}px;height:{3};position:absolute;overflow:hidden;z-index:{4}'><object id='rpCont{6}.{7}Flash' style='z-index:{4};' codebase='http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=7,0,19,0' width={0} height={3}><param name='movie' value='{2}' /><param name='quality' value='high' /><param name='allowscriptaccess' value='always'><param name='wmode' value='{5}' /><embed allowscriptaccess='always' id='rpCont{6}.{7}FlashEmbed' style='position:relative;z-index:{4};' src='{2}' width={0} height={3} allowScriptAccess='sameDomain' allowFullScreen='false' quality='high' pluginspage='http://www.macromedia.com/go/getflashplayer' type='application/x-shockwave-flash' wmode='{5}'></embed></object></div>"//<div id='spacerForFF' style='width:{0}px;height:{1}px;z-index:{4};'></div>";
_templates["bg"] = "<div align='left' id='divSticky{4}' name='divSticky' style='position:{5};top:0;left:{3}px;width:{1}px;height:{2};visibility:visible;z-index:2147483647'><a href='{0}' title='TIKLAYIN!' target='_blank' style='display:block;width:{1}px;height:{2};'><img border='0' src='" + _rpdom + "img/e.gif' width='{1}px' height='{2}'/></a></div>";
_templates["videobg"] = "<div style='position:fixed;width:{1}px;height:{2}px;z-index:{6};top:{7}px;left:{8}px;' id='rpCont{4}.{5}'><object style='position:relative;padding:0;left:0px;top:0px;z-index:{6};' height='{2}px' width='{1}px' align='top' id='rpCont{4}.{5}Flash' codebase='http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=5,0,0,0'><param name='movie' value='{0}'><param name='quality' value='high'><param name='menu' value='false'><param name='wmode' value='{3}'><param name='allowscriptaccess' value='always'><param name='FlashVars' value='clickTAG={9}'><embed allowscriptaccess='always' id='rpCont{4}.{5}FlashEmbed' src='{0}' quality='high' menu='false' wmode='{3}' pluginspage='http://www.macromedia.com/shockwave/download/index.cgi?P1_Prod_Version=ShockwaveFlash' type='application/x-shockwave-flash' width='{1}' height='{2}'></embed></object></div>";
var templates = {
    hrefImg: '<a href="' + clkUrl + '" target="_blank" id="rpCont{0}.{1}"><img src="{3}" border="0" width="{4}" height="{5}" alt="TIKLAYIN"></a>',
    swf: '<div id="rpCont{0}.{1}" style="width:{4}px;height:{5}px;z-index:{6};"{9}><object id="rpCont{0}.{1}Flash" width="{4}" height="{5}" data="{3}?{7}=' + clkUrlFL + '" type="application/x-shockwave-flash"{10}><param value="{3}?{7}=' + clkUrlFL + '" name="movie"><param value="always" name="allowscriptaccess"><param value="high" name="quality"><param value="transparent" name="wmode"><param value="high" name="quality"><param value="noborder" name="scale"><param value="{8}" name="FlashVars">{11}</object></div>',
    noswf: '<div id="rpCont{0}.{1}" style="width:{4}px;height:{5}px;z-index:{6};"{9}><div id="rpCont{0}.{1}Flash" style="width:{4}px;height:{5}px;z-index:{6};"{10}><a href="' + clkUrl + '" target="_blank" title="TIKLAYIN"><img src="{3}" alt="tıklayın" style="width:{4}px;height:{5}px;" title="tıklayın"{10} /></a></div></div>',
    shareUrl: http + 'ad.reklamport.com/share.ashx?ciid={0}&tid={1}&e={2}&ae={3}&su={4}&URL={5}',
    href: '<a id="rpCont{0}.{1}" href="{2}" target="_blank">{3}</a>',
    img: '<img id="rpCont{0}.{1}Img" src="{2}" border="0" width="{3}" height="{4}" alt="{5}" title="{6}" />'
};
var userag = _n.userAgent.toLowerCase();
var brow = new function () {
    this.id = 1; this.ff = false; this.chrome = false; this.ie = false; this.opera = false; this.safari = false; this.webkit = false; this.other = false;
    var mzn = /(firefox)[\/\s](\d+\.\d)/gim; var mzo = /(firefox)[\/\s](\d+\.\d+.\d+)/gim; var wbk = /(webkit)[ \/]([\w.]+)/gim; var cho = /(chrome)[ \/]([\w.]+)/gim; var opr = /(opera)(?:.*version)?[ \/]([\w.]+)/gim; var iea = /(msie) ([\w.]+)/gim; var saf = /(version)[ \/]([\w.]+)/gim;
    var brReg = cho.exec(userag) || opr.exec(userag) || iea.exec(userag) || userag.indexOf('compatible') < 0 && (mzo.exec(userag) || mzn.exec(userag)) || userag.indexOf('safari') < 0 && wbk.exec(userag) || [];
    if (brReg.length == 0 && userag.indexOf('safari') > 0) { brReg = saf.exec(userag) || []; brReg[1] = 'safari'; }
    var version = brReg[2] ? brReg[2].split('.')[0] : '0';
    switch (brReg[1]) { case 'firefox': this.ff = true; this.id = 2; break; case 'chrome': this.chrome = true; this.id = 3; break; case 'msie': this.ie = true; this.id = 4; break; case 'opera': this.opera = true; this.id = 5; break; case 'safari': this.safari = true; this.id = 6; break; case 'webkit': this.webkit = true; this.id = 7; break; default: this.other = true; break; }
    return { name: brReg[1] || '', fullver: brReg[2] || '0', ver: version, ff: this.ff, chrome: this.chrome, ie: this.ie, opera: this.opera, safari: this.safari, webkit: this.webkit, other: this.other, id: this.id };
};
function isValidUrl(s) { var regexp = /(ftp|http|https):\/\/(\w+:{0,1}\w*@)?(\S+)(:[0-9]+)?(\/|\/([\w#!:.?+=&%@!\-\/]))?/; return regexp.test(s); }
function rp_flashControl(_ciid, _tid, _clickUrl, _width, _height) { if (!rp_flashEnable()) { if (eval("typeof backup" + _ciid + "_" + _tid + " !== typeof undefined")) { var _imageUrl = eval("backup" + _ciid + "_" + _tid); if (isValidUrl(_imageUrl)) { _d.write(rpf(_templates["img"], _redurl, _ciid, _tid, _clickUrl, _imageUrl, _width, _height)); return false; } } } return true; }
function rp_flashEnable() { var flash = (navigator.mimeTypes && navigator.mimeTypes['application/x-shockwave-flash']) ? navigator.mimeTypes['application/x-shockwave-flash'].enabledPlugin ? true : false : false; if (!flash && brow.ie) { try { flash = (new ActiveXObject('ShockwaveFlash.ShockwaveFlash')) ? true : false; } catch (_e) { flash = false; } } return flash; }
function thirdRequest(_ciid, _tid) {
    if (eval("typeof request" + _ciid + "_" + _tid + " !== typeof undefined")) {
        var _requestUrl = eval("request" + _ciid + "_" + _tid);
        if (isValidUrl(_requestUrl)) {
            var rps = document.createElement('script');
            rps.async = true;
            rps.src = _requestUrl;// + (_requestUrl.indexOf("?") > 0 ? "&" : "?") + "_rnd=" + Math.random() % 99999999;
            //rps.width = 1, rps.height = 1; with (rps.style) { position = "absolute"; top = "-10px"; left = "-10px"; width = "1px"; height = "1px"; }
            if (_d.body) {
                var _rps = _d.body.getElementsByTagName('script')[0];
                _rps.parentNode.insertBefore(rps, _rps);
            }
        }
    }
}
function isEmpty(obj) { if (typeof obj == 'undefined' || obj === null || obj === '') return true; if (typeof obj == 'number' && isNaN(obj)) return true; if (obj instanceof Date && isNaN(Number(obj))) return true; return false; }
function scrsize(inner) {
    var scw = 0, sch = 0;
    if (inner) {
        if (typeof (_w.innerWidth) == "number") { scw = _w.innerWidth; sch = _w.innerHeight; }
        else if (_d.documentElement && (_d.documentElement.clientWidth || _d.documentElement.clientHeight)) { scw = _d.documentElement.clientWidth; sch = _d.documentElement.clientHeight; }
        else if (_d.body && (_d.body.clientWidth || _d.body.clientHeight)) { scw = _d.body.clientWidth; sch = _d.body.clientHeight; }
    }
    else {
        var wds = new Array(600, 800, 900, 1000, 1100, 1200, 1300, 1400, 1600, 1900), hts = new Array(400, 500, 600, 700, 800, 900, 1000, 1200);
        scw = wds[0]; sch = hts[0];
        for (var i = wds.length; i >= 0; i--) { if (_s.width > wds[i]) { scw = wds[i]; break; } }
        for (var i = hts.length; i >= 0; i--) { if (_s.height > hts[i]) { sch = hts[i]; break; } }
    }
    return { w: scw, h: sch };
}
function rpAddEvent(el, evname, func) { if (el.attachEvent) el.attachEvent("on" + evname, func); else if (el.addEventListener) { el.addEventListener(evname, func, true); } else el["on" + evname] = func; }
function CreateLink(href, title, html) { var l = document.createElement('a'); l.target = '_blank'; l.href = href; l.innerHTML = html; return l; }
function SocialBannerShareBar(ciid, tid, clk, sw, sh, bbc, bop, bpos) {
    var id = 'rpCont' + ciid + '.' + tid, socialLink = false, rpCont = rpget(id), socialDiv = document.createElement("div");
    socialDiv.id = id + 'SocialDiv', zi = parseInt(rpCont.style.zIndex) + 1;
    socialDiv.setAttribute("style", "height: " + sh + "px; width: " + sw + "px;text-align:" + bpos + "; position: absolute; background-color:" + bbc + "; opacity: " + bop + "; z-index: " + zi + "; padding: 10px; margin-top: -50px; box-shadow: 0px 10px 6px -6px " + bbc + " inset; display: none;");
    if (typeof (fbProp.title) != 'undefined' && fbProp.title != '') {
        var fblink = templates.shareUrl.Format(ciid, tid, fbProp.e, fbProp.ae, fbProp.su, fbProp.url.Format(encodeURI(fbProp.title), encodeURI(fbProp.summary), escape(fbProp.image), escape(clk)));
        var fbh = CreateLink(fblink, fbProp.linkTitle, templates.img.Format(ciid, tid, fbProp.logo, '32', '32', fbProp.linkTitle, fbProp.linkTitle));
        socialDiv.appendChild(fbh); socialLink = true;
    }
    if (typeof (twitProp.title) != 'undefined' && twitProp.title != '') {
        var twitlink = templates.shareUrl.Format(ciid, tid, twitProp.e, twitProp.ae, twitProp.su, twitProp.url.Format(encodeURI(twitProp.title), escape(clk)));
        var twh = CreateLink(twitlink, twitProp.linkTitle, templates.img.Format(ciid, tid, twitProp.logo, '32', '32', twitProp.linkTitle, twitProp.linkTitle));
        socialDiv.appendChild(twh); socialLink = true;
    }
    if (typeof (pinProp.title) != 'undefined' && pinProp.title != '') {
        var pinlink = templates.shareUrl.Format(ciid, tid, pinProp.e, pinProp.ae, pinProp.su, pinProp.url.Format(escape(pinProp.image), encodeURI(pinProp.title), escape(clk)));
        var pinh = CreateLink(pinlink, pinProp.linkTitle, templates.img.Format(ciid, tid, pinProp.logo, '32', '32', pinProp.linkTitle, pinProp.linkTitle));
        socialDiv.appendChild(pinh); socialLink = true;
    }
    if (typeof (gogProp.title) != 'undefined' && gogProp.title != '') {
        var goglink = templates.shareUrl.Format(ciid, tid, gogProp.e, gogProp.ae, pinProp.su, gogProp.url.Format(escape(clk)));
        var gogh = CreateLink(goglink, gogProp.linkTitle, templates.img.Format(ciid, tid, gogProp.logo, '32', '32', gogProp.linkTitle, gogProp.linkTitle));
        socialDiv.appendChild(gogh); socialLink = true;
    }
    if (socialLink) { rpCont.onmouseover = function () { SocialBannerBarShow(socialDiv.id, 1); }; rpCont.onmouseout = function () { SocialBannerBarShow(socialDiv.id, 0); }; rpCont.appendChild(socialDiv); }
}
function SocialBannerBarShow(id, d) { var rpBar = rpget(id); rpBar.style.display = d ? 'block' : 'none'; }
function SocialBanner(ciid, tid, clk, ctg, src, bis, w, h, zi, sw, sh, bbc, bop, bpos, dwr, aid) {

    thirdRequest(ciid, tid);
    if (!rp_flashControl(ciid, tid, clk, w, h))
        return;

    clk = clk.replace('http://', http); src = src.replace('http://', http);
    var creaCode = rp_flashEnable() ? templates.swf.Format(ciid, tid, clk, src, w, h, zi, ctg, '', '', '', '') : templates.noswf.Format(ciid, tid, escape(clk), bis, w, h, zi, ctg, '', '', '');
    if (dwr == 0 || dwr == '0') { document.write(creaCode); } else { var ex = rpget(aid); if (ex) { ex.innerHTML = creaCode; } else { document.write(creaCode); } }
    var xt = setTimeout("SocialBannerShareBar(" + ciid + ", " + tid + ", '" + clk + "', " + sw + ", " + sh + ", '" + bbc + "', '" + bop + "', '" + bpos + "')", 500);
}
function rp_Toggle(a, w, h, p) {
    var d = document, e = 10, f = d.getElementById(a), isIe = navigator.appName.toLowerCase().indexOf('explorer') >= 0;
    var ow = isIe ? f.offsetWidth : f.clientWidth, oh = isIe ? f.offsetHeight : f.clientHeight;
    var i = (oh > h ? oh : h) / e, z = (ow > w ? ow : w) / e, jw = 0, jh = 0, qw = false, qh = false, rsw = ow != w, rsh = oh != h, nW = 0, nH = 0, nml = 0;
    var k = window.setInterval(function () {
        if (rsw) { jw++; if (jw > z) { jw = z; qw = true; } else { nW = parseInt(ow + jw * ((w - ow) / z)) + "px"; nml = '-' + (jw * ((w - ow) / z)) + 'px'; f.style.width = nW; if (p == 2) f.style.marginLeft = nml; } } else { qw = true; }
        if (rsh) { jh++; if (jh > i) { jh = i; qh = true; } else { nH = parseInt(oh + jh * ((h - oh) / i)) + "px"; f.style.height = nH; } } else { qh = true; }
        if (qw && qh) { window.clearInterval(k); f.style.width = w + "px"; f.style.height = h + "px"; if (p == 2) f.style.marginLeft = (w - ow) > 0 ? '-' + (w - ow) + 'px' : '0px'; }
    }, e);
}
var richAction = false;
function rp_expandableAction(i, w, h) { rp_Toggle(i, w, h, 1); rmlog(richAction); richAction = !richAction; }
function rp_expandablePushIt(ciid, tid, curl, src, w, h, mw, mh, zi, ctg, oe) {
    _rmcid = ciid; _rmtid = tid;
    var de = '', fv = '', flh = '<div id="rpCont{0}.{1}" style="position: relative; width:{4}px; height:{5}px;overflow:hidden;display:block;margin:auto;z-index:{6};"{9}><div id="rpCont{0}.{1}In" style="width:100%;height:100%"><object id="rpCont{0}.{1}Flash" width="{4}" height="{10}" data="{3}?{7}=' + _redurl + '?c={0}%26t={1}%26URL={2}" type="application/x-shockwave-flash"><param value="{3}?{7}=' + _redurl + '?c={0}%26t={1}%26URL={2}" name="movie"><param value="always" name="allowscriptaccess"><param value="high" name="quality"><param value="transparent" name="wmode"><param value="high" name="quality"><param value="noborder" name="scale"><param value="{8}" name="flashvars"></object></div></div>';
    switch (oe.toString()) {
        case 'c': de = rpf(" onmouseout=\"rp_expandableAction('rpCont{4}.{5}',{0},{1},1)\" onclick=\"rp_expandableAction('rpCont{4}.{5}',{2},{3},1)\"", w, h, mw, mh, ciid, tid); break;
        case 'o': de = rpf(" onmouseout=\"rp_expandableAction('rpCont{4}.{5}',{0},{1},1)\" onmouseover=\"rp_expandableAction('rpCont{4}.{5}',{2},{3},1)\"", w, h, mw, mh, ciid, tid); break;
        default: fv = rpf('func=rp_expandableAction&objid=rpCont{0}.{1}&width={2}&height={3}&maxwidth={4}&maxheight={5}', ciid, tid, w, h, mw, mh); break;
    }
    var htm = rpf(flh, ciid, tid, curl, src, w, h, zi, ctg, fv, de, mh);
    document.write(htm);
}
function rpget(id) { if (typeof _d.getElementById(id) == "undefined") return null; return _d.getElementById(id); }
function rp_moveit(id) { var movdiv = rpget(id); if (movdiv) _d.getElementsByTagName('body')[0].appendChild(movdiv); }
function rpf() {
    if (arguments.length == 0) return "";
    var xtr = arguments[0];
    for (var i = 1; i < arguments.length; i++) xtr = xtr.replace(new RegExp('\\{' + (i - 1) + '\\}', 'gm'), arguments[i]);
    return xtr;
}
function rp_RedURL(_ciid, _tid, _clkurl) { return rpf("{0}?c={1}%26t={2}%26URL={3}", _redurl, _ciid, _tid, escape(_clkurl).replace('&', '&amp;')); }
function rp_GetSwf(_imgurl, _ciid, _tid, _clkurl, clkTg) { return rpf("{0}?{5}={1}?c={2}%26t={3}%26URL={4}", _imgurl, _redurl, _ciid, _tid, escape(_clkurl).replace('&', '&amp;'), clkTg); }
function rpGetPageTop() { return typeof _w.pageYOffset != 'undefined' ? _w.pageYOffset : _d.documentElement.scrollTop ? _d.documentElement.scrollTop : _d.body.scrollTop ? _d.body.scrollTop : 0; }
function rpScrollAll() {
    var _arrst = _stickyid.split(","), _st;
    for (var i = 0; i < _arrst.length; i++) { _st = rpget(_arrst[i]); if (_st) _st.style.top = rpGetPageTop() + "px"; }
}
function rp_getWidth() { return (_isIE ? 0 : -16) + rp_filter(_w.innerWidth ? _w.innerWidth : 0, _d.documentElement ? _d.documentElement.clientWidth : 0, _d.body ? _d.body.clientWidth : 0); }
function rp_getHeight() { return rp_filter(_w.innerHeight ? _w.innerHeight : 0, _d.documentElement ? _d.documentElement.clientHeight : 0, _d.body ? _d.body.clientHeight : 0); }
function rp_def(r_v, r_d) { return (typeof (r_v) == "undefined") ? r_d : r_v; }
function rp_max() { var _rpm = arguments[0]; for (var i = 0; i < arguments.length; i++) { if (_rpm < arguments[i]) { _rpm = arguments[i]; } } return _rpm; }
function rp_filter(n_win, n_docel, n_body) { return rp_max(rp_def(n_win, 0), rp_def(n_docel, 0), rp_def(n_body, 0)); }
function rp_closeOverlay(id) { rpget(id + "Span").style.display = "none"; rpget(id + "Link").style.display = "none"; rpget(id).style.display = "none"; rpget(id + "Close").style.display = "none"; rpget(id + "Close2").style.display = "none"; if (rpgoto) top.location.href = rpgoto; }
function rp_standart(_ciid, _tid, _clkurl, _imgurl, _width, _height, _ftype, _wmode, _zindex, clkTg, _crid, _rnd) {

    thirdRequest(_ciid, _tid);
    if (!rp_flashControl(_ciid, _tid, _clkurl, _width, _height))
        return;

    _crid = _crid || 0; _rnd = _rnd || 0; _wmode = _wmode || "transparent"; _zindex = _zindex || 100; clkTg = clkTg || "clickTAG";
    switch (_ftype) {
        case "img": _d.write(rpf(_templates["img"], _redurl, _ciid, _tid, _clkurl, _imgurl, _width, _height)); break;
        case "swf": _d.write(rpf(_templates["swf"], rp_GetSwf(_imgurl, _ciid, _tid, _clkurl, clkTg), _width, _height, _wmode, _ciid, _tid, _zindex, rp_RedURL(_ciid, _tid, _clkurl))); break;
        case "rollover": _d.write(rpf(_templates["rollover"], _width, _height, rp_GetSwf(_imgurl, _ciid, _tid, _clkurl, clkTg), _height, _zindex, _wmode, _ciid, _tid)); break;
        case "expandable": _d.write(rpf(_templates["expandable"], rp_GetSwf(_imgurl, _ciid, _tid, _clkurl, clkTg), _width, _height, _wmode, _ciid, _tid, _zindex, _divStl, rp_RedURL(_ciid, _tid, _clkurl))); break;
    }
}
function $RP_html(_ciid, _tid, _clkurl, _imgurl, _width, _height, _ftype, _wmode, _zindex, clkTg, _crid, _rnd) {
    thirdRequest(_ciid, _tid);
    if (!rp_flashControl(_ciid, _tid, _clkurl, _width, _height))
        return;

    _crid = _crid || 0; _rnd = _rnd || 0;
    _vcid = _ciid; _vtid = _tid;
    if (!_wmode) _wmode = "transparent";
    if (!_zindex) _zindex = "100";
    if (!clkTg) clkTg = "clickTAG";
    switch (_ftype) {
        case "img": return rpf(_templates["img"], _redurl, _ciid, _tid, _clkurl, _imgurl, _width, _height); break;
        case "swf": return rpf(_templates["swf"], rp_GetSwf(_imgurl, _ciid, _tid, _clkurl, clkTg), _width, _height, _wmode, _ciid, _tid, _zindex, rp_RedURL(_ciid, _tid, _clkurl)); break;
        case "rollover": return rpf(_templates["rollover"], _width, _height, rp_GetSwf(_imgurl, _ciid, _tid, _clkurl, clkTg), _height, _zindex, _wmode, _ciid, _tid); break;
        case "expandable": return rpf(_templates["expandable"], rp_GetSwf(_imgurl, _ciid, _tid, _clkurl, clkTg), _width, _height, _wmode, _ciid, _tid, _zindex, _divStl, rp_RedURL(_ciid, _tid, _clkurl)); break;
    }
}
function rp_sticky(_ciid, _tid, _clkurl, _imgurl, _width, _height, _ftype, _wmode, _zindex, clkTg, _leftSp, _topSp) {
    var winW = rp_getWidth(), winH = rp_getHeight(), _leftSp = _leftSp == 0 ? ((winW - _width) / 2) : _leftSp, _topSp = _topSp == 0 ? ((winH - _height) / 2) : _topSp;
    rp_standart(_ciid, _tid, _clkurl, _imgurl, _width, _height, _ftype, _wmode, _zindex, clkTg);
    var rpid = rpf("rpCont{0}.{1}", _ciid, _tid);
    _stickyid += rpid;
    var _cnt = rpget(rpid);
    if (_cnt) { with (_cnt.style) { position = "absolute"; left = _leftSp + "px"; top = _topSp + "px"; } }
    _w.onscroll = rpScrollAll;
}
function rp_floating(_ciid, _tid, _clkurl, _imgurl, _width, _height, _leftSp, _topSp, _ftype, _wmode, _zindex, clkTg, _clsSec, _clsBtn) {
    rpClsSec = _clsSec || 8;
    var winW = rp_getWidth(), winH = rp_getHeight(), _leftSp = _leftSp == 0 ? ((winW - _width) / 2) : _leftSp, _topSp = _topSp == 0 ? ((winH - _height) / 2) : _topSp;
    var rpid = rpf("rpCont{0}.{1}", _ciid, _tid), msg = "";
    if (_clsBtn == "1") msg = "Kapat";
    _d.write(rpf("<div id='{0}Close' style='background-color:transparent;position:absolute;top:{1};padding:0px;left:{2};width:{3};height:{4}px;z-index:{5}'></div>", rpid, _topSp, _leftSp, _width, _height, _zindex));
    rp_standart(_ciid, _tid, _clkurl, _imgurl, _width, _height, _ftype, _wmode, _zindex, clkTg)
    _d.write(rpf("<div style='position:absolute;left:{0}px;top:{1}px;width:{2}px;text-align:right;z-index:{3}9'>", _leftSp, _topSp - 15, _width, _zindex));
    _d.write(rpf("<span id='{0}Span' style='text-decoration:none;padding-top:10px;color:#000000;font-weight:bold;font-family:Tahoma;font-size:11px;'></span>", rpid));
    _d.write(rpf("<a href=# id='{0}Link' style='text-decoration:none;padding-top:10px;color:#000000;font-weight:bold;font-family:Tahoma;font-size:11px;' onclick='rp_closeOverlay(&quot;{0}&quot;);'>{1}</a></div>", rpid, msg));
    var _cnt = rpget(rpid);
    if (_cnt) { with (_cnt.style) { position = "absolute"; left = _leftSp + "px"; top = _topSp + "px"; } }
    if (_clsSec > 0) _w.setTimeout("rp_closeOverlay('" + _cnt.id + "');", rpClsSec * 1000);
}
function rp_rollover(_ciid, _tid, _clkurl, _imgurl, _width, _height, _maxH, _wmode, _zindex, clkTg) {
    var rpid = rpf("rpCont{0}.{1}", _ciid, _tid);
    rp_standart(_ciid, _tid, _clkurl, _imgurl, _width, _maxH, "rollover", _wmode, _zindex, clkTg);
    var rpEmbed = rpget(rpid + "FlashEmbed"); var _cnt = rpget(rpid), rpflash = rpget(rpid + "Flash");
    _cnt.onmouseover = function () { rp_rolloverStyle(this, _width, _maxH); }
    _cnt.onmouseout = function () { rp_rolloverStyle(this, _width, _height); }
    rp_rolloverStyle(_cnt, _width, _height);
}
function rp_rolloverStyle(cnt, _newW, _newH) { with (cnt.style) { width = _newW + "px"; height = _newH + "px"; } }
function rp_void() { return; }
function rp_overlay(_ciid, _tid, _clkurl, _imgurl, _width, _height, _ftype, clkTg, _bgColor, _color, _zindex, _wmode, _leftSp, _topSp) {

    thirdRequest(_ciid, _tid);
    if (!rp_flashEnable()) {
        if (eval("typeof backup" + _ciid + "_" + _tid + " !== typeof undefined")) {
            var _imageUrl = eval("backup" + _ciid + "_" + _tid);
            if (isValidUrl(_imageUrl)) {
                _ftype = "img";
                _imgurl = _imageUrl;
            }
        }
    }

    var _html = "", _bgColor = _bgColor || "#000"; _color = _color || "#fff";
    clkTg = clkTg || "clickTAG"; _zindex = _zindex || 99; _topSp = _topSp || 0;
    _wmode = _wmode || "transparent";
    switch (_ftype) {
        case "img": _html = rpf(_templates["img"], _redurl, _ciid, _tid, _clkurl, _imgurl, _width, _height); break;
        case "swf": _html = rpf(_templates["swf"], rp_GetSwf(_imgurl, _ciid, _tid, _clkurl, clkTg), _width, _height, _wmode, _ciid, _tid, _zindex, rp_RedURL(_ciid, _tid, _clkurl)); break;
    }
    var rpid = rpf("rpCont{0}.{1}", _ciid, _tid);
    _html = rpf("<div id='" + rpid + "Close' style='background-color:" + _bgColor + ";position:absolute;top:0;padding:10px;left:0;width:100%;height:4096px;z-index:{3}'></div>" +
            "{0}<div id='" + rpid + "Close2' style='position:absolute;left:{1}px;top:{2}px;width:" + _width + "px;text-align:right;z-index:{3}9'>" +
            "<br><br><span id='" + rpid + "Span' style='text-decoration:none;color:" + _color + ";font-weight:bold;font-family:Tahoma;font-size:11px;'></span>" +
            "<a href='' id='" + rpid + "Link' style='text-decoration:none;color:" + _color + ";font-weight:bold;font-family:Tahoma;font-size:11px;' onclick='rp_closeOverlay(&quot;" + rpid + "&quot;); return false;'></a></div>",
            _html, _leftSp, _topSp - 40, _zindex);
    _d.write(_html);
    var _cnt = rpget(rpid);
    if (_cnt) { with (_cnt.style) { position = "absolute"; left = _leftSp + "px"; top = (20 + _topSp) + "px"; }; }
}
function rp_interstitial(_ciid, _tid, _clkurl, _imgurl, _width, _height, _disptype, _ftype, _bgColor, _color, _clsSec, _showCounter, _left, clkTg, _zindex, _wMode, _clsWin, _top) {
    rpClsSec = _clsSec || 8;
    _top = _top || 0;
    var winW = rp_getWidth(), winH = rp_getHeight(), _left = _left == 0 ? ((winW - _width) / 2) : _left, _top = _top == 0 ? ((winH - _height) / 2) : _top;
    rp_overlay(_ciid, _tid, _clkurl, _imgurl, _width, _height, _ftype, clkTg, _bgColor, _color, _zindex, _wMode, _left, _top);
    var rpid = rpf("rpCont{0}.{1}", _ciid, _tid);
    var _cnt = rpget(rpid);
    with (_cnt.style) { position = "absolute"; left = _left + "px"; top = _top + "px"; zIndex = _zindex + 1; }
    _showCounter = _showCounter || "1";
    _w.setTimeout("rp_closeOverlay('" + _cnt.id + "');", rpClsSec * 1000);
    if (_clsWin == "1") rpget(rpid + "Link").innerHTML += "Kapat";
    if (_showCounter == "1") { rpget(rpid + "Span").innerHTML = rpClsSec-- + " sn sonra kapanacak."; _w.setInterval("rpget('" + rpid + "Span').innerHTML=(rpClsSec--)+' sn sonra kapanacak.';", 1000); }
}
function rp_background(_ciid, _tid, _clkurl, _imgurl, _sitew, _cntrd) {

    thirdRequest(_ciid, _tid);

    _cntrd = (_cntrd != 0);
    var _winW = rp_getWidth(), _winH = _d.documentElement ? _d.documentElement.clientHeight : rp_getHeight(), _bglink = rpf("{0}?c={1}&t={2}&URL={3}", _redurl, _ciid, _tid, _clkurl),
        _bgh = rpf("<style>body{background-image:url('{0}');background-repeat:no-repeat;background-attachment:fixed;background-position:{1} top;}</style>", _imgurl, _cntrd ? "center" : "left");
    var _dw = Math.floor((_winW - _sitew) / 2), _rl = Math.floor((_winW + _sitew) / 2);
    _bgh += rpf(_templates["bg"], _bglink, (_cntrd ? _dw : 2 * _dw), _winH + "px", _cntrd ? _rl : _sitew, 2, _isIE ? "absolute" : "fixed");
    _stickyid += "divSticky2,";
    if (_cntrd) { _bgh += rpf(_templates["bg"], _bglink, _dw, _winH + "px", 0, 1, _isIE ? "fixed" : "fixed"); _stickyid += "divSticky1,"; }
    _d.write(_bgh);
    rp_moveit("divSticky2");
    if (_cntrd) rp_moveit("divSticky1");
    if (_isIE) { _stickyid = "divSticky2," + (_cntrd ? "divSticky1," : "") }
    _w.setInterval(function () {
        _winW = rp_getWidth(); _winH = _d.documentElement ? _d.documentElement.clientHeight : rp_getHeight();
        if (_winH < 1) _winH = rp_getHeight();
        var rpd1 = rpget("divSticky1"), rpd2 = rpget("divSticky2");
        var _dw = parseInt((_winW - _sitew) / 2), _nw = (_cntrd ? 1 : 2) * Math.max(0, _dw);
        if (rpd1) {
            with (rpd1.getElementsByTagName("a")[0].style) { height = _winH + "px"; width = _nw + "px"; }
            with (rpd1.getElementsByTagName("img")[0].style) { height = _winH + "px"; width = _nw + "px"; }
            with (rpd1.style) { height = _winH + "px"; width = _nw + "px"; }
        }
        with (rpd2.style) { left = (_cntrd ? parseInt((_winW + _sitew) / 2) : _sitew) + "px"; height = _winH + "px"; width = _nw + "px"; }
        with (rpd2.getElementsByTagName("a")[0].style) { height = _winH + "px"; width = _nw + "px"; }
        with (rpd2.getElementsByTagName("img")[0].style) { height = _winH + "px"; width = _nw + "px"; }
        if (_isIE) rpScrollAll();
    }, 100);
}
function rp_linktext(_ciid, _tid, _text1, _text2, _text3, _clkUrl, _font, _italic, _bold, _uline, _fcolor, _lcolor, _fsize, _fcolor2) {
    var _stl = rpf("font-family:{0};font-size:{1}px;{2}{3}{4}", _font, _fsize, _italic == 1 ? "font-style:italic;" : "", _bold == 1 ? "font-weight:bold;" : "", _uline == 1 ? "text-decoration:underline;" : "");
    _d.write(rpf("<span style='{0}color:{1}'>{2}</span><a href='{3}?c={4}&t={5}&URL={6}' style='{0}color:{7}' target=_blank>{8}</a><span style='{0}color:{10}'>{9}</span>",
        _stl, _fcolor, _text1, _redurl, _ciid, _tid, _clkUrl, _lcolor, _text2, _text3, _fcolor2));
}
function rp_expandable(_ciid, _tid, _clkurl, _imgurl, _width, _height, _maxW, _maxH, _wmode, _zindex, clkTg, _xpDir, _oe, _div) {
    var rpid = rpf("rpCont{0}.{1}", _ciid, _tid); var _objLeft = "0px";
    switch (_xpDir) {
        case "rightdown": _divStl = "left:0px;top:0px"; break;
        case "leftdown": _divStl = "right:0px;top:0px"; _objLeft = "-" + (_maxW - _width) + "px"; break;
        case "rightup": _divStl = "left:0px;bottom:0px"; break;
        case "leftup": _divStl = "left:0px;bottom:0px"; _objLeft = "-" + (_maxW - _width) + "px"; break;
    }
    if (typeof (_div) != 'undefined') {
        var crdv = $RP_html(_ciid, _tid, _clkurl, _imgurl, _width, _height, "expandable", _wmode, _zindex, clkTg, _maxH, _maxW);
        rpget(_div).innerHTML = crdv;
    }
    else {
        rp_standart(_ciid, _tid, _clkurl, _imgurl, _width, _height, "expandable", _wmode, _zindex, clkTg, _maxH, _maxW);
    }

    var _cnt = rpget(rpid), rpflash = rpget(rpid + "Flash"), rpEmbed = rpget(rpid + "FlashEmbed");
    if (!_isIE) {
        with (rpEmbed) { width = _maxW + "px"; height = _maxH + "px"; style.position = "absolute"; style.left = _objLeft; }
    }
    else {
        with (rpflash.style) { position = "absolute"; height = _maxH + "px"; width = _maxW + "px"; }
        if (typeof (_div) == 'undefined')
            _d.write(rpf("<div id='{0}fix' style='width:{1}px;line-height:0;height:0px;z-index:0'></div>", rpid, _width));
    }
    _expmover(_cnt, false, _height, _maxH, _width, _maxW);
    expandover = function () { _expmover(rpget(rpid), true, _height, _maxH, _width, _maxW) };
    expandout = function () { _expmover(rpget(rpid), false, _height, _maxH, _width, _maxW) };
    if (_oe != "x") {
        _cnt.onmouseover = expandover;
        _cnt.onmouseout = expandout;
    }
}
function _expmover(_div, _o, _height, _maxH, _width, _maxW) {
    if (_isIE) {
        //with ($RP(_div.id + "Flash").style) { height = (_maxH) + "px"; width = (_maxW) + "px"; }
        with (_div.style) { overflow = _o ? "visible" : "hidden"; }
    } else {
        with (_div.style) { overflow = _o ? "visible" : "hidden"; }
    }
}
function rp_html(ciid, tid, html) { _d.write(html); }
function RpBanner(tt, ciid, tid, vast, rm) {
    var ext = !isEmpty(ciid) ? '&ciid=' + ciid : ''; ext += !isEmpty(tid) ? '&tid=' + tid : ''; ext += !isEmpty(vast) ? '&VAST=1' : '';
    ext += !isEmpty(window.rpFlr) ? '&flr=' + window.rpFlr : '';
    if (typeof (rp_ext) !== "undefined") { for (_e in rp_ext) { ext += !isEmpty(rp_ext[_e]) ? '&' + _e + '=' + rp_ext[_e] : ''; } }
    var rnd = Math.random() % 99999999;
    if (pageRunMode == runmode.getad) { document.write('<script src="' + getad.Format(tt, ext, rnd) + '"></' + 'script>'); }
}

var DomReady = window.DomReady = {};
var userAgent = navigator.userAgent.toLowerCase();
var browser = { version: (userAgent.match(/.+(?:rv|it|ra|ie)[\/: ]([\d.]+)/) || [])[1], safari: /webkit/.test(userAgent), opera: /opera/.test(userAgent), msie: (/msie/.test(userAgent)) && (!/opera/.test(userAgent)), mozilla: (/mozilla/.test(userAgent)) && (!/(compatible|webkit)/.test(userAgent)) };
var readyBound = false; var isReady = false; var readyList = [];
function domReady() { if (!isReady) { isReady = true; if (readyList) { for (var fn = 0; fn < readyList.length; fn++) { readyList[fn].call(window, []); } readyList = []; } } };
function addLoadEvent(func) { var oldonload = window.onload; if (typeof window.onload != 'function') { window.onload = func; } else { window.onload = function () { if (oldonload) { oldonload(); } func(); } } };
function bindReady() { if (readyBound) { return; } readyBound = true; if (document.addEventListener && !browser.opera) { document.addEventListener("DOMContentLoaded", domReady, false); } if (browser.msie && window == top) (function () { if (isReady) return; try { document.documentElement.doScroll("left"); } catch (error) { setTimeout(arguments.callee, 0); return; } domReady(); })(); if (browser.opera) { document.addEventListener("DOMContentLoaded", function () { if (isReady) return; for (var i = 0; i < document.styleSheets.length; i++) if (document.styleSheets[i].disabled) { setTimeout(arguments.callee, 0); return; } domReady(); }, false); } if (browser.safari) { var numStyles; (function () { if (isReady) return; if (document.readyState != "loaded" && document.readyState != "complete") { setTimeout(arguments.callee, 0); return; } if (numStyles === undefined) { var links = document.getElementsByTagName("link"); for (var i = 0; i < links.length; i++) { if (links[i].getAttribute('rel') == 'stylesheet') { numStyles++; } } var styles = document.getElementsByTagName("style"); numStyles += styles.length; } if (document.styleSheets.length != numStyles) { setTimeout(arguments.callee, 0); return; } domReady(); })(); } addLoadEvent(domReady); };
DomReady.ready = function (fn, args) { bindReady(); if (isReady) { fn.call(window, []); } else { readyList.push(function () { return fn.call(window, []); }); } };
bindReady();

function getIframeDocument(tt) {
    var _ifrm = document.getElementById("frm" + tt);
    _ifrm = (_ifrm.contentWindow) ? _ifrm.contentWindow : (_ifrm.contentDocument.document) ? _ifrm.contentDocument.document : _ifrm.contentDocument;
    return _ifrm;
}

function iframeAdControl(tt, width, height, count, _class) {
    frm = getIframeDocument(tt);
    _isOk = false;
    if (frm && frm.document && frm.document.body && frm.document.body.childNodes.length > 4) {
        if (String(frm.document.body.innerHTML).indexOf("<!-- no ad -->") > -1)
            return;

        /*size-problem*/
        if (true) {
            var images = frm.document.body.getElementsByTagName("img");
            for (_i = 0; _i < images.length ; _i++) {
                height = images[_i].height;
                width = images[_i].width;
                if (isEmpty(height) || isEmpty(width) || height == 0 || width == 0) {
                    height = images[_i].style.height;
                    width = images[_i].style.width;
                }
                if (width > 0 && height > 0) {
                    _isOk = true;
                    break;
                }
            }

            var objects = frm.document.body.getElementsByTagName("object");
            if (objects.length > 0) {
                height = objects[0].height;
                width = objects[0].width;
                if (isEmpty(height) || isEmpty(width)) {
                    height = objects[0].style.height;
                    width = objects[0].style.width;
                }
                _isOk = true;
            }
            else {
                var embeds = frm.document.body.getElementsByTagName("embed");
                if (embeds.length > 0) {
                    height = embeds[0].height;
                    width = embeds[0].width;
                    if (isEmpty(height) || isEmpty(width)) {
                        height = embeds[0].style.height;
                        width = embeds[0].style.width;
                    }
                    _isOk = true;
                }
            }
        }
        /*size-problem*/

        if (_isOk) {
            var ifrm = document.getElementById("frm" + tt);
            ifrm.width = width;
            ifrm.height = height;

            div = document.getElementById("div" + tt);
            div.className = _class;
        }
    }

    if (!_isOk) {
        count++;
        if (count < 10)
            window.setTimeout("iframeAdControl('" + tt + "', " + width + "," + height + "," + count + ", '" + _class + "')", 500);
    }
}

function rpBanner_asy(tt, ciid, tid, width, height, _class) {
    document.write("<div id='div" + tt + "'><iframe id='frm" + tt + "' height='0' width='0' scrolling='no' marginheight='0' marginwidth='0' allowtransparency='true' frameborder='0' src=''></ifr" + "ame></div>");
    function ad_frm() {
        _ifrm = getIframeDocument(tt);
        var ext = !isEmpty(ciid) ? '&ciid=' + ciid : ''; ext += !isEmpty(tid) ? '&tid=' + tid : ''; ext += !isEmpty(window.rpFlr) ? '&flr=' + window.rpFlr : '';
        if (typeof (rp_ext) !== "undefined") { for (_e in rp_ext) { ext += !isEmpty(rp_ext[_e]) ? '&' + _e + '=' + rp_ext[_e] : ''; } }

        var rnd = Math.random() % 99999999;

        _ifrm.document.open();
        var mlt = 1;
        if (_isIE) {
            window.setTimeout('getIframeDocument("' + tt + '").document.write("<div style=\'width:1px;height:1px;position:absolute;top:-100px;\'>a</div>")', 400);
            window.setTimeout('getIframeDocument("' + tt + '").document.write("<sc" + "ript src=\'' + _rpdom + 'scripts/rp.js?v2.6\'></s" + "cript>")', 500);
            window.setTimeout('getIframeDocument("' + tt + '").document.write("<div style=\'width:1px;height:1px;position:absolute;top:-100px;\'>b</div>")', 700);
            window.setTimeout('getIframeDocument("' + tt + '").document.write("<sc" + "ript src=\'' + getad.Format(tt, ext, rnd) + '\'></s" + "cript>")', 900);
        }
        else {
            window.setTimeout('getIframeDocument("' + tt + '").document.write("<div style=\'width:1px;height:1px;position:absolute;top:-100px;\'>a</div>")', 10);
            window.setTimeout('getIframeDocument("' + tt + '").document.write("<sc" + "ript src=\'' + _rpdom + 'scripts/rp.js?v2.6\'></s" + "cript>")', 20);
            window.setTimeout('getIframeDocument("' + tt + '").document.write("<div style=\'width:1px;height:1px;position:absolute;top:-100px;\'>bc</div>")', 30);
            window.setTimeout('getIframeDocument("' + tt + '").document.write("<sc" + "ript src=\'' + getad.Format(tt, ext, rnd) + '\'></s" + "cript>")', 40);
            window.setTimeout('getIframeDocument("' + tt + '").document.close();', 900);
        }

        window.setTimeout("iframeAdControl('" + tt + "', " + width + "," + height + ", 0, '" + _class + "')", 200);
    }
    DomReady.ready(ad_frm);
    //rpAddEvent(window, "load", ad_frm);
}
/*document.write('<sc' + 'ript src=\"' + _rpdom + 'scripts/rpdh.js?v3\"></s' + 'cript>');*/