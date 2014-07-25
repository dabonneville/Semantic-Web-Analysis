$(document).ready(function () {

    Adv();
    var menu = $("#mainMenu");

    $("li.menuItem", menu).each(function (index, item) {
        $(this).hover(function () {
            $(".menuTooltip", menu).hide(0);
            $(".menuTooltip", item).show(0);

            //Üye girişini kapat
            $(".loginForm").hide();
        }, function () {
            $(".menuTooltip", menu).hide(0);

            //Üye girişini kapat
            $(".loginForm").hide();
        });
    });

    $("li.menuItemStatic", menu).each(function (index, item) {
        $(this).hover(function () {
            $(".menuTooltipStatic", menu).hide(0);
            $(".menuTooltipStatic", item).show(0);

            //Üye girişini kapat
            //$(".loginForm").hide();
        }, function () {
            $(".menuTooltipStatic", menu).hide(0);

            //Üye girişini kapat
            //$(".loginForm").hide();
        });
    });



    /************************************************************/
    //    $(".loginForm").mouseleave(function () {
    //        $(this).hide();
    //    });
    $("#sendPasswordFormClose").click(function () {
        $(".sendPasswordForm").hide();
    });
    $("#loginFormClose").click(function () {
        $(".loginForm").hide();
    });
    $(".loginFormOpen").click(function () {
        OpenLoginBox();
    });

    $(".registerFormOpen").click(function () {
        OpenRegisterForm();
    });

    $(".socialButtons .lg").click(function () {
        OpenRegisterForm();
    });

    $(".profileOpenForm").click(function () {
        OpenProfileUpdateForm();
    });
    // Sayfa yenilendiğinde beni hatırla butonunu ayarla
    RememberMe($("#rememberMe").is(':checked'));

    // Beni hatırla butonuna basınca
    $("#rememberMeButton").click(function () {
        RememberMe($("#rememberMe").is(':checked'));
    });


    /************************************************************/

    var myNewsContainer = $("#myNewsBox");
    if (myNewsContainer != undefined && myNewsContainer != null) {
        //        //Eger left position degeri ilk anda set edilmezse chrome da sorun oluyor. 
        //        var left = myNewsContainer.position().left;
        //        $('#myNewsBox').css('left', left + 'px');
        //        //

        $(".closeBtn", myNewsContainer).click(function () {
            CloseReadLaterBox();
        });

        $(".myNewsBoxButton", myNewsContainer).click(function () {
            if ($('#myNewsBox').is('.closed')) {
                OpenReadLaterBox();
            }
            else {
                CloseReadLaterBox();
            }
        });
    }
    $(window).resize(function () {
        var resizeLeft = $(window).width();
        if ($('#myNewsBox').is('.closed')) {
            $('#myNewsBox').css('right', '-197px');
        }
        else {
            $('#myNewsBox').css('right', '0px');
        }

    });

    /************************************************************/

    $(".is-stock").each(function (index, item) {
        $(this).hover(function () {
            var stock = $(this);
            var stockName = TrimString(stock.text());
            var div = $('#divStock_' + stockName);

            if (div.html() == null) {
                div = jQuery('<div>', { id: 'divStock_' + stockName, style: 'display:block;position:absolute;left:' + stock.position().left + 'px;' });
                div.insertAfter(stock);

                var containerTop = Math.round(stock.position().top);
                containerTop -= 30;
                div.offset({ top: containerTop });

                div.html('<div class="compTooltip back" style="height:317px;width:385px"><div style="position:relative;height:316px;"><img src="http://img-cdn.cnbce.com/assets/img/gfx/loading128.gif" alt="Yükleniyor..." style="top:125px;left:154px;position:absolute;"/></div></div>');
                div.show(0);

                $.ajax({
                    url: '/services/getstockquickinfo/' + stockName,
                    data: {},
                    dataType: 'html'
                }).done(function (data) {
                    div.html(data);
                });
            }
            else {
                div.show(0);
            }

            $('#divStock_' + stockName).mouseout(function () {
                CloseStockInfo(stockName);
                $('#divStock_' + stockName).unbind('mouseout');
            });
        });
    });

    $(".is-market").each(function (index, item) {
        $(this).hover(function () {
            var market = $(this);
            var marketName = TrimString(market.text());
            var div = $('#divMarket_' + marketName);

            if (div.html() == null) {
                div = jQuery('<div>', { id: 'divMarket_' + marketName, style: 'display:block;position:absolute;left:' + market.position().left + 'px;' });
                div.insertAfter(market);

                var containerTop = Math.round(market.position().top);
                containerTop -= 30;
                div.offset({ top: containerTop });

                div.html('<div class="compTooltip back" style="height:317px;width:385px"><div style="position:relative;height:316px;"><img src="http://img-cdn.cnbce.com/assets/img/gfx/loading128.gif" alt="Yükleniyor..." style="top:125px;left:154px;position:absolute;"/></div></div>');
                div.show(0);

                $.ajax({
                    url: '/services/getmarketquickinfo/' + marketName,
                    data: {},
                    dataType: 'html'
                }).done(function (data) {
                    div.html(data);
                });
            }
            else {
                div.show(0);
            }

            $('#divMarket_' + marketName).mouseout(function () {
                CloseMarketInfo(marketName);
                $('#divMarket_' + marketName).unbind('mouseout');
            });
        });
    });
    /************************************************************/

    $(".loginForm .logEnter").click(function () {
        var form = $(".loginForm");
        var userName = $("#username", form).val();
        var password = $("#password", form).val();
        var rememberMe = $("#rememberMe", form).is(':checked');

        if (userName == "") {
            alert("Lütfen email adresinizi girin.");
            return false;
        }

        if (password == "") {
            alert("Lütfen şifrenizi girin.");
            return false;
        }

        SignIn(userName, password, rememberMe);
    });

    $(".sendPasswordForm .sendPasswordEnter").click(function () {
        var form = $(".sendPasswordForm");
        var userName = $("#username", form).val();

        if (userName == "") {
            alert("Lütfen email adresinizi girin.");
            return false;
        }

        SendPassword(userName);
    });

    /************************************************************/

    /************************************************************/

    $("#btnHaberAra").click(function () {
        StartSearch($(".searchBox input").val());
    });

    $("#btnVideoAra").click(function () {
        StartVideoSearch($(".searchBox input").val());
    });

    $(".newsSearch input").keypress(function (event) {
        if (event.which == 13) {
            event.preventDefault();
            StartSearch($(this).val());
        }
    });

    $(".videoSearch input").keypress(function (event) {
        if (event.which == 13) {
            event.preventDefault();
            StartVideoSearch($(this).val());
        }
    });

    $("#stockSearch").autocomplete({
        source: function (request, response) {
            $.ajax({
                url: "/services/finance/searchstock",
                dataType: "json",
                data: { keyword: request.term },
                success: function (data) {
                    response($.map(data, function (item) {
                        return {
                            label: item.label,
                            id: item.id
                        };
                    }));
                }
            });
        },
        minLength: 2,
        select: function (event, ui) {
            window.document.location.href = '/piyasa/bist/hisse-senedi/' + ui.item.id;
        }
    });

    $(".animateSearchBox input").click(function () {
        $(this).val('');
        $(this).animate({
            width: "150px"
        }, 500);
    }).blur(function () {
        $(this).animate({
            width: "75px"
        }, 500);
    });

    $(".readitlater").click(function () {
        ReadLaterAction($(this));
    });

    $(".laterRead").click(function () {
        ReadLaterAction($(this));
    });

    //Readlaterları ekrana basar
    ReadLaterControl(null);

    $('#flna').click(function () {
        $('.memberAgree').load('/contentpart/accountaggrement', function () {
            $.blockUI({
                message: $('.memberAgree'),
                css: { top: '10%',
                    width: '600px',
                    left: '50%',
                    border: 'none',
                    marginLeft: '-300px'
                }
            });
            $('.memberAgree .close').attr('title', 'Kapat').click($.unblockUI);
        });

    });
});