var liveLock = false;
var lockCount = 0;
var loginRedirectUrl = '';
var liveHour = 0;
var liveMin = 0;
var liveSec = 0;
var clockOk = false;
var refreshTimer = null;
var watchListLiveLock = false;
var watchListLiveLockCount = 0;
/************************************************************/
function IsNumericFunc() {
    $(".integer").numeric(false, function () {
        alert("Sadece Rakam Giriniz.");
        this.value = "";
        this.focus();
    });
}
function OpenHeaderAdvertise() {
    $('.topBanner').show(0);
}
function CloseHeaderAdvertise() {
    $('.topBanner').hide(0);
}
function TrimString(str) {
    return str.replace(/^\s\s*/, '').replace(/\s\s*$/, '');
}
function RefreshPage(delay) {
    if ($('body.no-bnr').length > 0) {
        return;
    }
    if (window.location.pathname.indexOf('/cnbc-e-tv/canli-izle') < 0)
        $.ajax({
            url: '/services/ad',
            dataType: 'json',
            type: 'post'
        }).done(function (data) {
            if (data == "1") {
                refreshTimer = setTimeout("window.document.location.href = window.document.location.href;", delay);
            }
        });

}
function ClearRefreshPage() {
    if (refreshTimer != null) {
        clearTimeout(refreshTimer);
    }
}
function OpenSendNewsMailBox() {
    $('.mailGoBox').show(0);
};

function SendNewsByEmail() {
    var name = $('#mailName').val();
    var mailAddresses = $('#mailAddresses').val();
    var note = $('#mailNote').val();
    var newsID = $('#newsId').val();

    if (newsID == null || newsID == '') {
        alert("E-posta gönderilemedi! Lütfen daha sonra tekrar deneyiniz.");
        return;
    }
    if (name == null || name == '' || mailAddresses == null || mailAddresses == '') {
        alert("Adınız ve Gönderilecek E-posta adreslerini doldurmalısınız.");
        return;
    }

    $.ajax({
        url: '/services/email/sharenews',
        data: {
            newsId: newsID,
            name: name,
            addresses: mailAddresses,
            note: note
        },
        dataType: 'json',
        type: 'post'
    }).done(function (data) {
        if (data == "1") {
            alert("Haber arkadaşlarınıza E-posta olarak gönderildi.");
            CloseSendNewsMailBox();
        }
        else {
            alert("E-posta gönderilemedi! Lütfen daha sonra tekrar deneyiniz.");
        }

    });
}

function CloseSendNewsMailBox() {
    $('.mailGoBox').hide(0);
};

function OpenProfileUpdateForm(redirectUrl) {
    ClearRefreshPage();
    if (redirectUrl != undefined && redirectUrl != null) {
        loginRedirectUrl = redirectUrl;
    }
    $.ajax({
        url: '/member/ProfileUpdate',
        dataType: 'html',
        type: 'get'
    }).done(function (data) {
        $("#profile").html(data);
    });

}

function CloseProfileUpdateForm() {
    $('.profileUpdateForm').hide(0);
}
function IntializeProfileUpdateForm() {
    var container = $(".profileUpdateForm");

    $('#countrySelect', container).change(function () {
        GetCities($(this).val(), container);
    });

    $('.gender', container).click(function () {
        SelectGender($(this), container);
    });

    $('#agreement', container).click(function () {
        SelectAgreement();
    });

    $('.updateButton', container).click(function () {
        ProfileUpdate();
    });

    $('.formClose', container).click(function () {
        CloseProfileUpdateForm();
    });

    $('#newsletter').click(function () {
        SelectNewsletter();
    });

}

function ProfileUpdate() {
    var password = $.trim($('#registerPassword').val());
    var passworConfirm = $.trim($('#registerPasswordConfirm').val());
    var name = $.trim($('#name').val());
    var surname = $.trim($('#surname').val());
    var email = $.trim($('#email').val());
    var country = $('#countrySelect').val();
    var city = $('#citySelect').val();
    var profession = $('#professionSelect').val();
    var birthDateDay = $('#daySelect').val();
    var birthDateMonth = $('#monthSelect').val();
    var birthDateYear = $('#yearSelect').val();
    var gender = '';
    var type = $('#updateType').val();
    var address = $('#address').val();

    if ($('#genderDiv input:checked').length == 1) {
        gender = $('#genderDiv input:checked').val();
    }

    if (password != passworConfirm) {
        alert('Girmiş olduğunuz şifreler birbirinden farklı');
        return;
    }

    if (name.length == 0 || surname.length == 0) {
        alert("Yanında * bulunan alanlar doldurulmak zorundadır");
        return;
    }

    if (!isValidEmailAddress(email)) {
        alert('Email adresi doğru formatta olmalı Ör:(abc@cnbce.com)');
        return;
    }

    if (password.length > 0) {
        if (!isValidPassword(password)) {
            alert('Şifre 8 ile 20 karakter olmalı. Harf ve sayı içermelidir.');
            return;
        }
    }

    var newsletter = $('#newsletter input:checkbox').is(':checked');

    var birthDate = null;
    if (birthDateDay != '0' && birthDateMonth != '0' && birthDateYear != '0') {
        birthDate = birthDateDay + '.' + birthDateMonth + '.' + birthDateYear;
    }

    if (type == 'anlik') {
        if (country == '0' || city == '0' || address.length == 0) {
            alert('* ve **  işaretli alanları doldurmalısınız.');
            return;
        }
    }

    $.ajax({
        url: '/services/member/update',
        data: {
            Password: password,
            PasswordConfirm: passworConfirm,
            FirstName: name,
            LastName: surname,
            Email: email,
            CountryID: country,
            CityId: city,
            ProfessionID: profession,
            Gender: gender,
            BirthDate: birthDate,
            Address: address,
            type: type,
            newsletter: newsletter
        },
        dataType: 'json',
        type: 'post'
    }).done(function (data) {
        if (data == "1") {
            alert("Hesap bilgileriniz güncellendi.");
            if (loginRedirectUrl != '') {
                window.document.location.href = loginRedirectUrl;
            }
            else {
                window.document.location.href = '/';
            }
        }
        else if (data == "0") {
            alert("Güncelleme işleminiz yapılamadı! Lütfen bilgilerinizi kontrol edip tekrar deneyiniz");
        }
        else {
            alert(data);
        }
    });


}

function OpenRegisterForm() {
    ClearRefreshPage();
    $(".loginForm").hide(0);
    $(".sendPasswordForm").hide(0);
    $(".socialForm").hide(0);
    var container = $(".registerForm");
    GetCountries(container);
    GetProfessions(container);
    GetYears(container);
    $('.registerForm #countrySelect').change(function () {
        GetCities($(this).val(), container);
    });

    $('.registerForm .gender').click(function () {
        SelectGender($(this), container);
    });

    $('.registerForm #agreement').click(function () {
        SelectAgreement();
    });

    $('.registerForm #newsletter').click(function () {
        SelectNewsletter();
    });

    $('.registerForm .registerButton').click(function () {
        Register();
    });

    $('.registerForm .formClose').click(function () {
        CloseRegisterForm();
    });

    $('.registerForm').show(0);
}

function CloseRegisterForm() {
    $('.registerForm').hide(0);
}

function Register() {
    var password = $.trim($('#registerPassword').val());
    var passworConfirm = $.trim($('#registerPasswordConfirm').val());
    var name = $.trim($('#name').val());
    var surname = $.trim($('#surname').val());
    var email = $.trim($('#email').val());
    var country = $('#countrySelect').val();
    var city = $('#citySelect').val();
    var profession = $('#professionSelect').val();
    var birthDateDay = $('#daySelect').val();
    var birthDateMonth = $('#monthSelect').val();
    var birthDateYear = $('#yearSelect').val();
    var gender = '';
    var address = $('#address').val();

    if ($('#genderDiv input:checked').length == 1) {
        gender = $('#genderDiv input:checked').val();
    }
    var agreement = $('#agreement input:checkbox').is(':checked');

    if (!agreement) {
        alert('Üyelik sözleşmesini kabul etmeden üye olamazsınız');
        return;
    }

    if (password != passworConfirm) {
        alert('Girmiş olduğunuz şifreler birbirinden farklı');
        return;
    }

    if (password.length == 0 || name.length == 0 || surname.length == 0) {
        alert("Yanında * bulunan alanlar doldurulmak zorundadır");
        return;
    }

    if (!isValidEmailAddress(email)) {
        alert('Email adresi doğru formatta olmalı Ör:(abc@cnbce.com)');
        return;
    }

    if (!isValidPassword(password)) {
        alert('Şifre 8 ile 20 karakter olmalı. Harf ve sayı içermelidir.');
        return;
    }

    var newsletter = $('#newsletter input:checkbox').is(':checked');

    var birthDate = null;
    if (birthDateDay != '0' && birthDateMonth != '0' && birthDateYear != '0') {
        birthDate = birthDateDay + '.' + birthDateMonth + '.' + birthDateYear;
    }

    $.ajax({
        url: '/services/member/register',
        data: {
            Password: password,
            PasswordConfirm: passworConfirm,
            FirstName: name,
            LastName: surname,
            Email: email,
            CountryID: country,
            CityId: city,
            ProfessionID: profession,
            Gender: gender,
            agreement: agreement,
            BirthDate: birthDate,
            Address: address,
            newsletter: newsletter
        },
        dataType: 'json',
        type: 'post'
    }).done(function (data) {
        if (data == "1") {
            alert("Üyelik işleminiz tamamlandı. Üye giriş bölümünden giriş yapabilirsiniz.");
            window.location.reload(true);
        }
        else if (data == "0") {
            alert("Üyelik işleminiz yapılamadı! Lütfen bilgilerinizi kontrol edip tekrar deneyiniz");
        }
        else if (data == "exists") {
            alert("Girmiş olduğunuz email adresi sistemde kayıtlı.");
        } else {
            alert(data);
        }
    });
}

function SelectAgreement() {
    var isChecked = $('#agreement input:checkbox').is(':checked');
    if (isChecked) {
        $('#agreement input:checkbox').attr('checked', false);
        $('#agreement span').removeClass('tick').addClass('untick');
    }
    else {
        $('#agreement input:checkbox').attr('checked', true);
        $('#agreement span').removeClass('untick').addClass('tick');
    }
}

function SelectNewsletter() {
    var isChecked = $('#newsletter input:checkbox').is(':checked');
    if (isChecked) {
        $('#newsletter input:checkbox').attr('checked', false);
        $('#newsletter span').removeClass('tick').addClass('untick');
    }
    else {
        $('#newsletter input:checkbox').attr('checked', true);
        $('#newsletter span').removeClass('untick').addClass('tick');
    }
}

function SelectGender(cnt, container) {
    $('#genderDiv input:checkbox', container).attr('checked', false);
    $('#genderDiv span', container).removeClass('tick').addClass('untick');

    $('input:checkbox', cnt).attr('checked', true);
    $('span', cnt).removeClass('untick').addClass('tick');
}

function AddOption(cnt, value, text, selected) {
    cnt.append($("<option></option>")
         .attr("value", value)
         .text(text));

    if (selected) {
        cnt.val(value);
    }
}
function GetCountries(container) {
    $.ajax({
        url: '/services/member/countries',
        dataType: 'json',
        type: 'post'
    }).done(function (data) {
        var select = $('#countrySelect', container);
        $('option', select).remove();
        AddOption(select, '0', 'Seçiniz', true);

        $.each(data, function (index, data) {
            AddOption(select, data.Value, data.Text, false);
        });
    });
}

function GetProfessions(container) {
    $.ajax({
        url: '/services/member/professions',
        dataType: 'json',
        type: 'post'
    }).done(function (data) {
        var select = $('#professionSelect', container);
        $('option', select).remove();
        AddOption(select, '0', 'Seçiniz', true);

        $.each(data, function (index, data) {
            AddOption(select, data.Value, data.Text, false);
        });
    });
}

function GetYears(container) {
    $.ajax({
        url: '/services/member/years',
        dataType: 'json',
        type: 'post'
    }).done(function (data) {
        var select = $('#yearSelect', container);
        $('option', select).remove();
        AddOption(select, '0', 'Yıl', true);

        $.each(data, function (index, data) {
            AddOption(select, data.Value, data.Text, false);
        });
    });
}

function GetCities(c, container) {
    $.ajax({
        url: '/services/member/cities',
        data: { countryId: c },
        dataType: 'json',
        type: 'post'
    }).done(function (data) {
        var select = $('#citySelect', container);
        $('option', select).remove();
        AddOption(select, '0', 'Seçiniz', true);

        $.each(data, function (index, data) {
            AddOption(select, data.Value, data.Text, false);
        });
    });
}

function isValidEmailAddress(emailAddress) {
    var pattern = new RegExp(/^[+a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$/i);
    return pattern.test(emailAddress);
};

function isValidPassword(password) {
    var pattern = new RegExp(/^.*(?=.{8,20})(?=.*\d)(?=.*[a-zA-Z]).*$/i);
    return pattern.test(password);
};

function StartSearch(keyword) {
    if (keyword == null || keyword == '') {
        alert("Arama yapacağınız kelimeyi kutuya girmelisiniz.");
        return;
    }

    window.location.href = '/arama/haber/' + encodeURIComponent(keyword);
}

function StartVideoSearch(keyword) {
    if (keyword == null || keyword == '') {
        alert("Arama yapacağınız kelimeyi kutuya girmelisiniz.");
        return;
    }

    window.location.href = '/video/arama/' + encodeURIComponent(keyword);
}

function OpenSendPasswordBox() {
    ClearRefreshPage();
    // Tüm açık menüleri gizle
    $(".menuTooltip").hide();
    $(".menuTooltipStatic").hide();
    $('.registerForm').hide(0);
    $('.loginForm').hide(0);

    $.scrollTo("#mainHeader", 900);
    // Giriş formunu aç
    $(".sendPasswordForm").show(0);
}

function OpenLoginBox(redirectUrl) {
    ClearRefreshPage();
    if (redirectUrl != undefined && redirectUrl != null) {
        loginRedirectUrl = redirectUrl;
    }
    // Tüm açık menüleri gizle
    $(".menuTooltip").hide();
    $(".menuTooltipStatic").hide();
    $('.registerForm').hide(0);
    $('.sendPasswordForm').hide(0);
    $('.socialForm').hide(0);

    $.scrollTo("#mainHeader", 900);
    // Giriş formunu aç
    $(".loginForm").show();

    $(".forgetPass").click(function () {
        OpenSendPasswordBox();
    });
}

function SignIn(u, p, r) {
    $.ajax({
        url: '/services/member/signin',
        data: { userName: u, password: p, rememberMe: r },
        dataType: 'json',
        type: 'post'
    }).done(function (data) {
        if (data.result) {
            if (loginRedirectUrl != '') {
                window.document.location.href = loginRedirectUrl;
            }
            else {
                window.location.reload(true);
            }
        }
        else {
            alert('Giriş başarısız');
        }
    });
}

function SendPassword(u) {
    $.ajax({
        url: '/services/member/sendpassword',
        data: { userName: u },
        dataType: 'json',
        type: 'post'
    }).done(function (data) {
        if (data == 1) {
            alert("Email adresiniz sistemimizde bulunmuyor. Yeni üye hesabı açabilirsiniz.");
        }
        else if (data == 2) {
            alert('Şifre hatırlatma bilgileri email adresinize gönderildi.');
            $(".sendPasswordForm").hide(0);
        }
        else if (data == 3) {
            alert('Üzgünüz işleminiz tamamlanamadı!');
        }
    });
}

function RememberMe(isCheck) {
    if (isCheck) {
        $("#rememberMeButton").find("span").removeClass("untick").addClass("tick");
    }
    else {
        $("#rememberMeButton").find("span").removeClass("tick").addClass("untick");
    }
}

function lastMinutePrev() {
    var slider = $("#sectionSlider");
    var slideIndex = $("ul:visible", slider).index();
    if (slideIndex == 0) {
        slideIndex = $("ul", slider).length;
    }
    $("ul", slider).hide(0);
    $("ul", slider).eq(slideIndex - 1).show(0);

}

function lastMinuteNext() {
    var slider = $("#sectionSlider");
    var slideIndex = $("ul:visible", slider).index();
    if (slideIndex == ($("ul", slider).length - 1)) {
        slideIndex = -1;
    }
    $("ul", slider).hide(0);
    $("ul", slider).eq(slideIndex + 1).show(0);

}

/*Davos*/
function lastMinuteDavosPrev() {
    var slider = $("#davosSlider");
    var slideIndex = $("ul:visible", slider).index();
    if (slideIndex == 0) {
        slideIndex = $("ul", slider).length;
    }
    $("ul", slider).hide(0);
    $("ul", slider).eq(slideIndex - 1).show(0);

}

function lastMinuteDavosNext() {
    var slider = $("#davosSlider");
    var slideIndex = $("ul:visible", slider).index();
    if (slideIndex == ($("ul", slider).length - 1)) {
        slideIndex = -1;
    }
    $("ul", slider).hide(0);
    $("ul", slider).eq(slideIndex + 1).show(0);

}

/*Davos*/

function ReadLaterAction(cnt) {
    if (cnt.is('.added')) {
        RemoveReadLater(cnt);
    }
    else {
        AddReadLater(cnt);
    }
}

function AddReadLater(cnt) {
    if (!isLogin) {
        OpenLoginBox();
    } else {
        var id = $("#newsId", cnt.parent()).val();
        cnt.fadeOut('200', function () {
            if (id != undefined && id != null) {
                $.ajax({
                    url: '/services/readlater/add',
                    data: { newsId: id },
                    dataType: 'json',
                    type: 'post'
                }).done(function (data) {
                    if (data == '1') {
                        OpenReadLaterBox();
                        RefreshReadLaterBox();
                        cnt.addClass('added');
                        $('span', cnt).html('Okudum');
                    } else {
                        alert('Haber eklenemedi! Lütfen daha sonra tekrar deneyiniz.');
                    }
                    cnt.fadeIn('500');
                });
            }
        });
    }
}

function RemoveReadLater(cnt) {
    var id = $("#newsId", cnt.parent()).val();
    cnt.fadeOut('200', function () {
        if (id != undefined && id != null) {
            $.ajax({
                url: '/services/readlater/remove',
                data: { newsId: id },
                dataType: 'json',
                type: 'post'
            }).done(function (data) {
                if (data == '1') {
                    RefreshReadLaterBox();
                    cnt.removeClass('added');
                    $('span', cnt).html('Sonra Oku');
                }
                else {
                    alert('İşleminiz tamamlanamadı! Lütfen daha sonra tekrar deneyiniz.');
                }
                cnt.fadeIn('500');
            });
        }
    });
}

function ReadLaterIsAdded(cnt) {
    var id = $("#newsId", cnt.parent()).val();

    if (id != undefined && id != null) {
        $.ajax({
            url: '/services/readlater/IsAdded',
            data: { newsId: id },
            dataType: 'json',
            type: 'post'
        }).done(function (data) {
            if (data == '1') {
                cnt.addClass('added');
                $('span', cnt).html('Okudum');
            }
            else {
                cnt.removeClass('added');
                $('span', cnt).html('Sonra Oku');
            }

        });
    }

}


function ReadLaterControl(container) {

    $.ajax({
        url: '/services/readlater/List',
        dataType: 'json',
        type: 'post'
    }).done(function (data) {
        if (data == '0') {
        }
        else {
            var readLater = null;
            $.each(data, function (index, data) {
                if (container != undefined && container != null) {
                    readLater = $('.readitlater[code="' + data + '"]', container);
                    if (readLater != undefined && readLater != null) {
                        readLater.addClass('added');
                        $('span', readLater).html('Okudum');
                    }
                    readLater = $('.laterRead[code="' + data + '"]', container);
                    if (readLater != undefined && readLater != null) {
                        readLater.addClass('added');
                        $('span', readLater).html('Okudum');
                    }
                }
                else {
                    readLater = $('.readitlater[code="' + data + '"]');
                    if (readLater != undefined && readLater != null) {
                        readLater.addClass('added');
                        $('span', readLater).html('Okudum');
                    }
                    readLater = $('.laterRead[code="' + data + '"]');
                    if (readLater != undefined && readLater != null) {
                        readLater.addClass('added');
                        $('span', readLater).html('Okudum');
                    }
                }
            });
        }

    });
}


function OpenReadLaterBox() {
    if (!isLogin) {
        OpenLoginBox();
    } else {

        $('#myNewsBox').animate({
            'right': '0px'
        }, 400, 'linear', function () {
        });
        $('#myNewsBox').addClass('opened');
        $('#myNewsBox').removeClass('closed');
        $('.myNewsBoxButton').addClass('myNewsBoxButtonClosed');
    }
}

function CloseReadLaterBox() {
    $('#myNewsBox').animate({
        'right': '-197px'
    }, 400, 'linear', function () {
    });
    $('#myNewsBox').addClass('closed');
    $('#myNewsBox').removeClass('opened');
    $('.myNewsBoxButton').removeClass('myNewsBoxButtonClosed');
}

function RefreshReadLaterBox() {
    var isOpen = $('#myNewsBox').is('.opened');
    $.ajax({
        url: '/ContentPart/ReadLater',
        data: {},
        dataType: 'html'
    }).done(function (data) {
        var div = $('#myNewsRefreshArea');
        div.html(data);
    });
}

function setTab(index, tabContainer, dataContainer) {

    $('li.isMenu', tabContainer).removeClass('selected');
    $('li.isMenu', tabContainer).eq(index).addClass('selected');

    $('div.tabContent', dataContainer).hide(0);
    $('div.tabContent', dataContainer).eq(index).show(0);
}

function setTabWatchListTab(index, tabContainer, dataContainer) {

    $('li.isMenu', tabContainer).removeClass('selected');
    $('li.isMenu', tabContainer).addClass('buttons');
    $('li.isMenu', tabContainer).eq(index).addClass('selected');
    $('li.isMenu', tabContainer).eq(index).removeClass('buttons');

    $('div.tabContent', dataContainer).hide(0);
    $('div.tabContent', dataContainer).eq(index).show(0);
}

function setTabChart(index, tabContainer) {

    $('li.isMenu', tabContainer).removeClass('selected');
    $('li.isMenu', tabContainer).eq(index).addClass('selected');
}

function CloseStockInfo(stockName) {
    $('#divStock_' + stockName).hide(0);
}
function CloseMarketInfo(stockName) {
    $('#divMarket_' + stockName).hide(0);
}

function LiveListIsAddedToWatchList(type, group, selector) {
    $.ajax({
        url: '/services/watchlist/GetTypeWatchList',
        data: { type: type },
        dataType: 'json',
        type: 'post'
    }).done(function (data) {
        if (data.length > 0) {
            var cont = null;
            $.each(data, function (index, data) {
                cont = $('span[code="' + data.FinanceCode + '"]');
                if (cont != undefined && cont != null) {
                    cont.removeClass('posiPlus');
                    cont.addClass('negaMinus');
                    cont.html('x');
                }
            });
        }

        $('.' + selector).click(function () {
            if ($(this).is('.posiPlus')) {
                AddWatchListLive($(this), group, $(this).attr('code'), type);
            }
            else {
                RemoveWatchListLive($(this), group, $(this).attr('code'));
            }
        });
    });
}

function WatchListLiveAction(cnt, group, code, type) {
    if (cnt.is('.negaMinus')) {
        RemoveWatchListLive(cnt, group, code);
    }
    else {
        AddWatchListLive(cnt, group, code, type);
    }
}

function AddWatchListLive(cnt, group, code, type) {
    if (!isLogin) {
        OpenLoginBox();
    } else {

        if (code != undefined && code != null) {
            $.ajax({
                url: '/services/watchlist/add',
                data: { code: code, group: group, type: type },
                dataType: 'json',
                type: 'post'
            }).done(function (data) {
                if (data == '1') {
                    GetLiveWatchList();
                    cnt.addClass('negaMinus');
                    cnt.removeClass('posiPlus');
                    cnt.html('x');
                } else {
                    alert('İşleminiz tamamlanamadı! Lütfen daha sonra tekrar deneyiniz.');
                }

            });
        }

    }
}

function RemoveWatchListLive(cnt, group, code) {
    if (code != undefined && code != null) {
        $.ajax({
            url: '/services/watchlist/remove',
            data: { code: code, group: group },
            dataType: 'json',
            type: 'post'
        }).done(function (data) {
            if (data == '1') {
                GetLiveWatchList();
                cnt.removeClass('negaMinus');
                cnt.addClass('posiPlus');
                cnt.html('+');
            }
            else {
                alert('İşleminiz tamamlanamadı! Lütfen daha sonra tekrar deneyiniz.');
            }
        });
    }
}


function WatchListButtonAction(cnt, group, code, type) {
    if ($('span', cnt).is('.added')) {
        RemoveWatchListButton(cnt, group, code);
    }
    else {
        AddWatchListButton(cnt, group, code, type);
    }
}

function AddWatchListButton(cnt, group, code, type) {
    if (!isLogin) {
        OpenLoginBox();
    } else {
        cnt.fadeOut('200', function () {
            if (code != undefined && code != null) {
                $.ajax({
                    url: '/services/watchlist/add',
                    data: { code: code, group: group, type: type },
                    dataType: 'json',
                    type: 'post'
                }).done(function (data) {
                    if (data == '1') {
                        //                        RefreshWatchListBox();

                        $('span', cnt).html('Sayfamdan Çıkar');
                        $('span', cnt).addClass('added')
                    } else {
                        alert('İşleminiz tamamlanamadı! Lütfen daha sonra tekrar deneyiniz.');
                    }
                    cnt.fadeIn('500');
                });
            }
        });
    }
}

function WatchListButtonIsAdded(cnt, type, code) {
    if (code != undefined && code != null) {
        $.ajax({
            url: '/services/watchlist/IsAdded',
            data: { code: code, type: type },
            dataType: 'json',
            type: 'post'
        }).done(function (data) {
            if (data == '1') {

                $('span', cnt).html('Sayfamdan Çıkar');
                $('span', cnt).addClass('added')
            } else {

                $('span', cnt).html('Sayfama Ekle');
                $('span', cnt).removeClass('added')
            }
            cnt.fadeIn('500');
        });
    }
}

function RemoveWatchListButton(cnt, group, code) {
    cnt.fadeOut('200', function () {
        if (code != undefined && code != null) {
            $.ajax({
                url: '/services/watchlist/remove',
                data: { code: code, group: group },
                dataType: 'json',
                type: 'post'
            }).done(function (data) {
                if (data == '1') {
                    //                    RefreshWatchListBox();

                    $('span', cnt).html('Sayfama Ekle');
                    $('span', cnt).removeClass('added')
                }
                else {
                    alert('İşleminiz tamamlanamadı! Lütfen daha sonra tekrar deneyiniz.');
                }
                cnt.fadeIn('500');
            });
        }
    });
}


/*Chart*/
function DrawChart(chartContainer, chartData, drawOptions, addCaption) {
    if (chartData == undefined || chartData == null || chartData.length == 0) {
        chartData = [['', ''], ['1', 1]];
    }
    else if (addCaption) {
        chartData.splice(0, 0, ['', '']);
    }
    data = google.visualization.arrayToDataTable(chartData);
    chartContainer.draw(data, drawOptions);
}

function GetChartData(type, code, callback) {
    if (code != null) {
        $.ajax({
            url: '/services/finance/Get' + type,
            data: { code: code },
            dataType: 'json',
            type: 'post'
        }).done(function (data) {
            var dataArray = new Array(0)
            if (data.Data != null) {
                dataArray = new Array(data.Data.length);
                $.each(data.Data, function (index, data) {
                    dataArray[index] = [data.Date, data.Level];
                });
            }
            callback(dataArray, data);
        });
    }
    else {
        $.ajax({
            url: '/services/finance/Get' + type,
            dataType: 'json',
            type: 'post'
        }).done(function (data) {
            var dataArray = new Array(0)
            if (data.Data != null) {
                dataArray = new Array(data.Data.length);
                $.each(data.Data, function (index, data) {
                    dataArray[index] = [data.Date, data.Level];
                });
            }
            callback(dataArray, data);
        });
    }
}

function GetCustomDateChartData(type, code, start, end, callback) {
    if (code != null) {
        $.ajax({
            url: '/services/finance/Get' + type,
            data: { code: code, startDate: start, endDate: end },
            dataType: 'json',
            type: 'post'
        }).done(function (data) {
            var dataArray = new Array(0)
            if (data.Data != null) {
                dataArray = new Array(data.Data.length);
                $.each(data.Data, function (index, data) {
                    dataArray[index] = [data.Date, data.Level];
                });
            }
            callback(dataArray, data);
        });
    }
    else {
        $.ajax({
            url: '/services/finance/Get' + type,
            data: { startDate: start, endDate: end },
            dataType: 'json',
            type: 'post'
        }).done(function (data) {
            var dataArray = new Array(0)
            if (data.Data != null) {
                dataArray = new Array(data.Data.length);
                $.each(data.Data, function (index, data) {
                    dataArray[index] = [data.Date, data.Level];
                });
            }
            callback(dataArray, data);
        });
    }
}

function GetConsumptionChartData(type, period, callback) {
    $.ajax({
        url: '/services/cnbceendeks/GetConsumptionChart',
        data: { type: type, period: period },
        dataType: 'json',
        type: 'post'
    }).done(function (data) {
        var dataArray = new Array(0)
        if (data != null) {
            dataArray = new Array(data.length);
            $.each(data, function (index, data) {
                dataArray[index] = [data.Date, data.Value];
            });
        }
        callback(dataArray, data);
    });

}

function GetConsumerTrustChartData(type, period, callback) {
    $.ajax({
        url: '/services/cnbceendeks/GetConsumerTrustChart',
        data: { type: type, period: period },
        dataType: 'json',
        type: 'post'
    }).done(function (data) {
        var dataArray = new Array(0)
        if (data != null) {
            dataArray = new Array(data.length);
            $.each(data, function (index, data) {
                dataArray[index] = [data.Date, data.Value];
            });
        }
        callback(dataArray, data);
    });

}

function GetSign(upDown) {
    if (upDown == 1) {
        return '+';
    } else {
        return '';
    }
}
function GetFinanceRightUpDownArrow(a) {
    switch (a) {
        case 0:
            return 'blue';
            break;
        case 1:
            return 'green';
            break;
        case -1:
            return 'red';
            break;
        default:
            return '';
            break;
    }
}
function GetFinanceCurrentUpDownArrow(a) {
    switch (a) {
        case 0:
            return 'bigBlue';
            break;
        case 1:
            return 'bigGreen';
            break;
        case -1:
            return 'bigRed';
            break;
        default:
            return '';
            break;
    }
}
function GetFinancePercentUpDownArrow(a) {
    switch (a) {
        case 0:
            return 'blue';
            break;
        case 1:
            return 'green';
            break;
        case -1:
            return 'red';
            break;
        default:
            return '';
            break;
    }
}
function GetImkbCurrentUpDownArrow(a) {
    switch (a) {
        case 0:
            return 'bigBlue';
            break;
        case 1:
            return 'bigGreen';
            break;
        case -1:
            return 'bigRed';
            break;
        default:
            return '';
            break;
    }
}
function GetImkbPercentUpDownArrow(a) {
    switch (a) {
        case 0:
            return 'blue';
            break;
        case 1:
            return 'green';
            break;
        case -1:
            return 'red';
            break;
        default:
            return '';
            break;
    }
}
function GetMarketCurrentUpDownArrow(a) {
    switch (a) {
        case 0:
            return 'bigBlue';
            break;
        case 1:
            return 'bigGreen';
            break;
        case -1:
            return 'bigRed';
            break;
        default:
            return '';
            break;
    }
}
function GetWorldMarketCurrentUpDownArrow(a) {
    switch (a) {
        case 0:
            return 'bigBlue';
            break;
        case 1:
            return 'bigGreen';
            break;
        case -1:
            return 'bigRed';
            break;
        default:
            return '';
            break;
    }
}

function GetGoldCurrentUpDownArrow(a) {
    switch (a) {
        case 0:
            return 'bigBlue';
            break;
        case 1:
            return 'bigGreen';
            break;
        case -1:
            return 'bigRed';
            break;
        default:
            return '';
            break;
    }
}

function GetAgricultureCurrentUpDownArrow(a) {
    switch (a) {
        case 0:
            return 'bigBlue';
            break;
        case 1:
            return 'bigGreen';
            break;
        case -1:
            return 'bigRed';
            break;
        default:
            return '';
            break;
    }
}

function GetEnergyCurrentUpDownArrow(a) {
    switch (a) {
        case 0:
            return 'bigBlue';
            break;
        case 1:
            return 'bigGreen';
            break;
        case -1:
            return 'bigRed';
            break;
        default:
            return '';
            break;
    }
}

function GetNewsCurrentUpDownArrow(a) {
    switch (a) {
        case 0:
            return 'bigBlue';
            break;
        case 1:
            return 'bigGreen';
            break;
        case -1:
            return 'bigRed';
            break;
        default:
            return '';
            break;
    }
}

function GetExchangeCurrentUpDownArrow(a) {
    switch (a) {
        case 0:
            return 'bigBlue';
            break;
        case 1:
            return 'bigGreen';
            break;
        case -1:
            return 'bigRed';
            break;
        default:
            return '';
            break;
    }
}
function GetFreeMarketCurrencyCurrentUpDownArrow(a) {
    switch (a) {
        case 0:
            return 'bigBlue';
            break;
        case 1:
            return 'bigGreen';
            break;
        case -1:
            return 'bigRed';
            break;
        default:
            return '';
            break;
    }
}

function GetMarketPercentUpDownArrow(a) {
    switch (a) {
        case 0:
            return 'blue big';
            break;
        case 1:
            return 'green big';
            break;
        case -1:
            return 'red big';
            break;
        default:
            return '';
            break;
    }
}
function GetWorldMarketPercentUpDownArrow(a) {
    switch (a) {
        case 0:
            return 'blue big';
            break;
        case 1:
            return 'green big';
            break;
        case -1:
            return 'red big';
            break;
        default:
            return '';
            break;
    }
}
function GetGoldPercentUpDownArrow(a) {
    switch (a) {
        case 0:
            return 'blue big';
            break;
        case 1:
            return 'green big';
            break;
        case -1:
            return 'red big';
            break;
        default:
            return '';
            break;
    }
}

function GetAgriculturePercentUpDownArrow(a) {
    switch (a) {
        case 0:
            return 'blue big';
            break;
        case 1:
            return 'green big';
            break;
        case -1:
            return 'red big';
            break;
        default:
            return '';
            break;
    }
}
function GetEnergyPercentUpDownArrow(a) {
    switch (a) {
        case 0:
            return 'blue big';
            break;
        case 1:
            return 'green big';
            break;
        case -1:
            return 'red big';
            break;
        default:
            return '';
            break;
    }
}
function GetNewsPercentUpDownArrow(a) {
    switch (a) {
        case 0:
            return 'blue big';
            break;
        case 1:
            return 'green big';
            break;
        case -1:
            return 'red big';
            break;
        default:
            return '';
            break;
    }
}
function GetExchangePercentUpDownArrow(a) {
    switch (a) {
        case 0:
            return 'blue big';
            break;
        case 1:
            return 'green big';
            break;
        case -1:
            return 'red big';
            break;
        default:
            return '';
            break;
    }
}
function GetFreeMarketCurrencyPercentUpDownArrow(a) {
    switch (a) {
        case 0:
            return 'blue big';
            break;
        case 1:
            return 'green big';
            break;
        case -1:
            return 'red big';
            break;
        default:
            return '';
            break;
    }
}
function GetStockCurrentUpDownArrow(a) {
    switch (a) {
        case 0:
            return 'bigBlue';
            break;
        case 1:
            return 'bigGreen';
            break;
        case -1:
            return 'bigRed';
            break;
        default:
            return '';
            break;
    }
}
function GetStockPercentUpDownArrow(a) {
    switch (a) {
        case 0:
            return 'blue big';
            break;
        case 1:
            return 'green big';
            break;
        case -1:
            return 'red big';
            break;
        default:
            return '';
            break;
    }
}
function GetCommodityCurrentUpDownArrow(a) {
    switch (a) {
        case 0:
            return 'bigBlue';
            break;
        case 1:
            return 'bigGreen';
            break;
        case -1:
            return 'bigRed';
            break;
        default:
            return '';
            break;
    }
}
function GetCommodityPercentUpDownArrow(a) {
    switch (a) {
        case 0:
            return 'blue big';
            break;
        case 1:
            return 'green big';
            break;
        case -1:
            return 'red big';
            break;
        default:
            return '';
            break;
    }
}
function GetInterestCurrentUpDownArrow(a) {
    switch (a) {
        case 0:
            return 'bigBlue';
            break;
        case 1:
            return 'bigGreen';
            break;
        case -1:
            return 'bigRed';
            break;
        default:
            return '';
            break;
    }
}
function GetInterestPercentUpDownArrow(a) {
    switch (a) {
        case 0:
            return 'blue big';
            break;
        case 1:
            return 'green big';
            break;
        case -1:
            return 'red big';
            break;
        default:
            return '';
            break;
    }
}
/*Chart*/

function ExchangeCalculate() {
    var firstEx = $('.converter #exhangeFirst').val();
    var secondEx = $('.converter #exhangeSecond').val();
    var amountEx = $('.converter #exchangeAmount').val();

    if (amountEx == "0") {
        alert("Miktar 0 dan büyük olmalı");
        return;
    }


    $.ajax({
        url: '/services/exchange/calculate',
        data: { first: firstEx, second: secondEx, amount: amountEx },
        dataType: 'json',
        type: 'post'
    }).done(function (data) {
        $('.converter #exchangeResult').html("= " + data);
    });
}


function GetCNBCeTVLastVideosSlider(cnt, count, isNext, imageSize) {
    var page = $('#pageNum', cnt).val();
    if (page == undefined || page == null) {
        page = 0;
    }
    page = isNext ? (parseInt(page) + 1) : (parseInt(page) - 1);

    $('#imageCnt', cnt).fadeOut('200', function () {
        $.ajax({
            url: '/services/cnbcetv/lastvideoList',
            data: { page: page, count: count, imageSize: imageSize, recursive: true },
            dataType: 'json',
            type: 'post'
        }).done(function (data) {
            if (data.length > 0) {
                $('#imageCnt', cnt).html('');

                $.each(data, function (index, data) {
                    $('#imageCnt', cnt).append('<li><a href="' + data.Url + '" title="' + data.Title + '" class="target"><img src="' + data.ImageUrl + '" title="' + data.Title + '" alt="' + data.Title + '" /><span>' + data.Title + '</span> </a></li>');
                    $('#pageNum', cnt).val(data.Page);
                });
            }

            $('#imageCnt', cnt).fadeIn('500');
            SetTargetBlank(cnt);
        });

    });
}

function GetLastSlideShowSlider(cnt, count, isNext, imageSize) {
    var page = $('#pageNum', cnt).val();
    if (page == undefined || page == null) {
        page = 0;
    }
    page = isNext ? (parseInt(page) + 1) : (parseInt(page) - 1);

    $('#imageCnt', cnt).fadeOut('200', function () {
        $.ajax({
            url: '/services/slide/lastslideshowlist',
            data: { page: page, count: count, imageSize: imageSize, recursive: true },
            dataType: 'json',
            type: 'post'
        }).done(function (data) {
            if (data.length > 0) {
                $('#imageCnt', cnt).html('');

                $.each(data, function (index, data) {
                    $('#imageCnt', cnt).append('<li><a href="' + data.Url + '" title="' + data.Title + '" class="target"><img src="' + data.ImageUrl + '" title="' + data.Title + '" alt="' + data.Title + '" /><span>' + data.Title + '</span> </a></li>');
                    $('#pageNum', cnt).val(data.Page);
                });
            }

            $('#imageCnt', cnt).fadeIn('500');
            SetTargetBlank(cnt);
        });

    });
}

function GetSaxoCapitalVideosSlider(cnt, count, isNext, imageSize) {
    var page = $('#pageNum', cnt).val();
    if (page == undefined || page == null) {
        page = 0;
    }
    page = isNext ? (parseInt(page) + 1) : (parseInt(page) - 1);

    $('#imageCnt', cnt).fadeOut('200', function () {
        $.ajax({
            url: '/services/cnbcetv/saxocapitalvideolist',
            data: { page: page, count: count, imageSize: imageSize, recursive: true },
            dataType: 'json',
            type: 'post'
        }).done(function (data) {
            if (data.length > 0) {
                $('#imageCnt', cnt).html('');

                $.each(data, function (index, data) {
                    $('#imageCnt', cnt).append('<li><a href="' + data.Url + '" title="' + data.Title + '" class="target"><img src="' + data.ImageUrl + '" title="' + data.Title + '" alt="' + data.Title + '" /><span>' + data.Title + '</span> </a></li>');
                    $('#pageNum', cnt).val(data.Page);
                });
            }

            $('#imageCnt', cnt).fadeIn('500');
            SetTargetBlank(cnt);
        });

    });
}

function GetVideos(cnt, count, isNext, imageSize, keyword, params) {
    var _defaults = {
        beforeSend: function () { },
        success: function () { }
    };
    var options = $.extend(_defaults, params);
    var page = $('#pageNum', cnt).val();
    if (page == undefined || page == null) {
        page = 0;
    }
    page = isNext ? (parseInt(page) + 1) : (parseInt(page) - 1);

    console.log(options);

    $('#imageCnt', cnt).fadeOut('200', function () {
        $.ajax({
            url: '/services/cnbcetv/videolist',
            data: { page: page, count: count, imageSize: imageSize, recursive: true, keyword: keyword },
            dataType: 'json',
            type: 'post',
            beforeSend: function () {
                options.beforeSend();
            }
        }).done(function (data) {
            if (data.length > 0) {
                $('#imageCnt', cnt).html('');

                $.each(data, function (index, data) {
                    $('#imageCnt', cnt).append('<li><a href="' + data.Url + '" title="' + data.Title + '" class="target"><img src="' + data.ImageUrl + '" title="' + data.Title + '" alt="' + data.Title + '" /><span>' + data.Title + '</span> </a></li>');
                    $('#pageNum', cnt).val(data.Page);
                });
                options.success();
            }

            $('#imageCnt', cnt).fadeIn('500');
            SetTargetBlank(cnt);
        });

    });
}
function GetCNBCeTVAllVideoList(cnt, count, isNext, imageSize, type, more) {
    var page = $('#pageNum', cnt).val();
    if (page == undefined || page == null) {
        page = 0;
    }
    page = isNext ? (parseInt(page) + 1) : (parseInt(page) - 1);

    $('#imageCnt', cnt).fadeOut('200', function () {
        $.ajax({
            url: '/services/cnbcetv/lastvideoList',
            data: { page: page, count: count, imageSize: imageSize, type: type },
            dataType: 'json',
            type: 'post'
        }).done(function (data) {
            if (data.length > 0) {
                if (more == false) {
                    $('#imageCnt', cnt).html('');
                }

                $.each(data, function (index, data) {
                    $('#imageCnt', cnt).append(' <div class="box"><a href="' + data.Url + '" class="photo"><img src="' + data.ImageUrl + '"  title="' + data.Title + '"  alt="' + data.Title + '" /></a><a href="' + data.Url + '" class="text" title="' + data.Title + '">' + data.Title + '</a></div>');
                    page = parseInt(data.Page);
                });

                $('#pageNum', cnt).val(page);
                $('#more', cnt).show(0);

                if (data.length < count) {
                    $('#more', cnt).hide(0);
                }
            }
            else {
                if (more == false) {
                    $('#imageCnt', cnt).html('');
                }
                $('#more', cnt).hide(0);
            }
            $('#imageCnt', cnt).fadeIn('500');
        });
    });
}

function GetCNBCeComSorunVideoList(cnt, count, isNext, imageSize, more) {
    var page = $('#pageNum', cnt).val();
    if (page == undefined || page == null) {
        page = 0;
    }
    page = isNext ? (parseInt(page) + 1) : (parseInt(page) - 1);

    $('#imageCnt', cnt).fadeOut('200', function () {
        $.ajax({
            url: '/services/cnbcetv/cnbcecomsorunvideoList',
            data: { page: page, count: count, imageSize: imageSize },
            dataType: 'json',
            type: 'post'
        }).done(function (data) {
            if (data.length > 0) {
                if (more == false) {
                    $('#imageCnt', cnt).html('');
                }

                $.each(data, function (index, data) {
                    $('#imageCnt', cnt).append(' <div class="box"><a href="' + data.Url + '" class="photo"><img src="' + data.ImageUrl + '"  title="' + data.Title + '"  alt="' + data.Title + '" /></a><a href="' + data.Url + '" class="text" title="' + data.Title + '">' + data.Title + '</a></div>');
                    page = parseInt(data.Page);
                });

                $('#pageNum', cnt).val(page);
                $('#more', cnt).show(0);

                if (data.length < count) {
                    $('#more', cnt).hide(0);
                }
            }
            else {
                if (more == false) {
                    $('#imageCnt', cnt).html('');
                }
                $('#more', cnt).hide(0);
            }
            $('#imageCnt', cnt).fadeIn('500');
        });
    });
}

/*Live*/
function GetLiveMarketUpDownArrow(a) {
    switch (a) {
        case 0:
            return 'bigBlue';
            break;
        case 1:
            return 'bigGreen';
            break;
        case -1:
            return 'bigRed';
            break;
        default:
            return '';
            break;
    }
}
function ClearChange() {
    $('.price').parent().removeClass('changeUp');
    $('.price').parent().removeClass('changeDown');
    $('.price').parent().removeClass('change');

    $('.bestBuy').parent().removeClass('changeUp');
    $('.bestBuy').parent().removeClass('changeDown');
    $('.bestBuy').parent().removeClass('change');

    $('.bestSell').parent().removeClass('changeUp');
    $('.bestSell').parent().removeClass('changeDown');
    $('.bestSell').parent().removeClass('change');

    $('.changePercent').parent().removeClass('changeUp');
    $('.changePercent').parent().removeClass('changeDown');
    $('.changePercent').parent().removeClass('change');

    $('.totalCount').parent().removeClass('changeUp');
    $('.totalCount').parent().removeClass('changeDown');
    $('.totalCount').parent().removeClass('change');

    $('.volume').parent().removeClass('changeUp');
    $('.volume').parent().removeClass('changeDown');
    $('.volume').parent().removeClass('change');

    $('.low1').parent().removeClass('changeUp');
    $('.low1').parent().removeClass('changeDown');
    $('.low1').parent().removeClass('change');

    $('.high1').parent().removeClass('changeUp');
    $('.high1').parent().removeClass('changeDown');
    $('.high1').parent().removeClass('change');

    $('.close1').parent().removeClass('changeUp');
    $('.close1').parent().removeClass('changeDown');
    $('.close1').parent().removeClass('change');

    $('.low2').parent().removeClass('changeUp');
    $('.low2').parent().removeClass('changeDown');
    $('.low2').parent().removeClass('change');

    $('.high2').parent().removeClass('changeUp');
    $('.high2').parent().removeClass('changeDown');
    $('.high2').parent().removeClass('change');

    $('.close2').parent().removeClass('changeUp');
    $('.close2').parent().removeClass('changeDown');
    $('.close2').parent().removeClass('change');
}

function GoLivePage(code) {
    window.document.location.href = '/anlik-piyasa/' + code;
}

function GetLiveWatchList() {
    $.ajax({
        url: '/services/finance/GetLiveWatchList',
        dataType: 'html',
        type: 'post'
    }).done(function (data) {
        $('#viewArea').html(data);

        $('.liveStockWatch', $('#viewArea')).click(function () {
            RemoveWatchListLiveWatch($(this), '2', $(this).attr('code'));
        });

        $('.liveMarketWatch', $('#viewArea')).click(function () {
            RemoveWatchListLiveWatch($(this), '1', $(this).attr('code'));
        });
    });
}

function RemoveWatchListLiveWatch(cnt, group, code) {
    if (code != undefined && code != null) {
        $.ajax({
            url: '/services/watchlist/remove',
            data: { code: code, group: group },
            dataType: 'json',
            type: 'post'
        }).done(function (data) {
            if (data == '1') {
                GetLiveWatchList();

                if (group == '1') {
                    $('span[code="' + code + '"]', $('#marketArea')).html('+');
                    $('span[code="' + code + '"]', $('#marketArea')).removeClass('negaMinus');
                    $('span[code="' + code + '"]', $('#marketArea')).addClass('posiPlus');
                }
                else if (group == '2') {
                    $('span[code="' + code + '"]', $('#stockArea')).html('+');
                    $('span[code="' + code + '"]', $('#stockArea')).removeClass('negaMinus');
                    $('span[code="' + code + '"]', $('#stockArea')).addClass('posiPlus');
                }
            }
            else {
                alert('İşleminiz tamamlanamadı! Lütfen daha sonra tekrar deneyiniz.');
            }
        });
    }
}

function UpdateLive() {
    if (liveLock) {
        lockCount = lockCount + 1;

        if (lockCount >= 4) {
            liveLock = false;
            lockCount = 0;
        }
        setTimeout("UpdateLive()", 1000);
    }
    else {
        liveLock = true;
        setTimeout("UpdateLive()", 3000);

        var code = $('#code').val();
        if ($('#liveViewOpen').is('.open')) {
            code = 'XUTUM';
        }
        $.ajax({
            url: '/services/finance/GetLiveMarket',
            data: { code: code },
            dataType: 'json',
            type: 'post'
        }).done(function (data) {
            if (data != null) {
                var updateTime = '';
                if (data.Markets != undefined && data.Markets != null && data.Stocks != undefined && data.Stocks != null) {
                    var marketArea = null;
                    var viewContainer = $('#viewArea');
                    updateTime = data.LastUpdate;
                    if (data.Markets.length > 0) {
                        var marketContainer = $('#marketArea');
                        var topMarketsContainer = $('.liveMarketTop');
                        $.each(data.Markets, function (index, market) {
                            marketArea = $('#market' + market.MarketCode, marketContainer);
                            topMarketArea = $('#topBox' + market.MarketCode, topMarketsContainer);
                            //Market
                            if (marketArea != undefined && marketArea != null) {
                                if ($('.price', marketArea).html() != market.Price) {
                                    $('.price', marketArea).html(market.Price);
                                    if (market.UpDown == 1) {
                                        $('.price', marketArea).parent().addClass('changeUp');
                                    }
                                    else if (market.UpDown == -1) {
                                        $('.price', marketArea).parent().addClass('changeDown');
                                    }
                                    else {
                                        $('.price', marketArea).parent().addClass('change');
                                    }
                                }

                                if ($('.upDown', marketArea).val() != market.UpDown) {
                                    $('.upDown', marketArea).val(market.UpDown);
                                    if (market.UpDown == 1) {
                                        $('.price', marketArea).parent().addClass('changeUp');
                                        $('.price', marketArea).attr('class', 'greenArrow price');
                                    }
                                    else if (market.UpDown == -1) {
                                        $('.price', marketArea).parent().addClass('changeDown');
                                        $('.price', marketArea).attr('class', 'redArrow price');
                                    }
                                    else {
                                        $('.price', marketArea).parent().addClass('change');
                                        $('.price', marketArea).attr('class', 'blueArrow price');
                                    }
                                }

                                if ($('.low1', marketArea).html() != market.Low1) {
                                    $('.low1', marketArea).html(market.Low1);
                                    $('.low1', marketArea).parent().addClass('change');
                                }

                                if ($('.high1', marketArea).html() != market.High1) {
                                    $('.high1', marketArea).html(market.High1);
                                    $('.high1', marketArea).parent().addClass('change');
                                }

                                if ($('.close1', marketArea).html() != market.Close1) {
                                    $('.close1', marketArea).html(market.Close1);
                                    $('.close1', marketArea).parent().addClass('change');
                                }

                                if ($('.low2', marketArea).html() != market.Low2) {
                                    $('.low2', marketArea).html(market.Low2);
                                    $('.low2', marketArea).parent().addClass('change');
                                }

                                if ($('.high2', marketArea).html() != market.High2) {
                                    $('.high2', marketArea).html(market.High2);
                                    $('.high2', marketArea).parent().addClass('change');
                                }

                                if ($('.close2', marketArea).html() != market.Close2) {
                                    $('.close2', marketArea).html(market.Close2);
                                    $('.close2', marketArea).parent().addClass('change');
                                }
                            }

                            //top
                            if (topMarketArea != undefined && topMarketArea != null) {
                                if ($('.boxPrice', topMarketArea).html() != market.Price) {
                                    $('.boxPrice', topMarketArea).html(market.Price);
                                    $('.boxPrice', topMarketArea).attr('class', GetLiveMarketUpDownArrow(market.UpDown) + ' boxPrice');
                                }


                                if ($('.boxLow', topMarketArea).html() != market.DayLow) {
                                    $('.boxLow', topMarketArea).html(market.DayLow);
                                }

                                if ($('.boxHigh', topMarketArea).html() != market.DayHigh) {
                                    $('.boxHigh', topMarketArea).html(market.DayHigh);
                                }

                            }

                            //Hızlı görünüme de bakar
                            if (viewContainer != undefined && viewContainer.is(':visible')) {

                                viewArea = $('#view' + market.MarketCode, viewContainer);

                                if (viewArea != undefined && viewArea != null) {
                                    if ($('.price', viewArea).html() != market.Price) {
                                        $('.price', viewArea).html(market.Price);
                                        if (market.UpDown == 1) {
                                            $('.price', viewArea).parent().addClass('changeUp');
                                        }
                                        else if (market.UpDown == -1) {
                                            $('.price', viewArea).parent().addClass('changeDown');
                                        }
                                        else {
                                            $('.price', viewArea).parent().addClass('change');
                                        }
                                    }

                                    if ($('.upDown', viewArea).val() != market.UpDown) {
                                        $('.upDown', viewArea).val(market.UpDown);
                                        if (market.UpDown == 1) {
                                            $('.price', viewArea).parent().addClass('changeUp');
                                            $('.price', viewArea).attr('class', 'greenArrow price');
                                        }
                                        else if (market.UpDown == -1) {
                                            $('.price', viewArea).parent().addClass('changeDown');
                                            $('.price', viewArea).attr('class', 'redArrow price');
                                        }
                                        else {
                                            $('.price', viewArea).parent().addClass('change');
                                            $('.price', viewArea).attr('class', 'blueArrow price');
                                        }
                                    }

                                    if ($('.low1', viewArea).html() != market.Low1) {
                                        $('.low1', viewArea).html(market.Low1);
                                        $('.low1', viewArea).parent().addClass('change');
                                    }

                                    if ($('.high1', viewArea).html() != market.High1) {
                                        $('.high1', viewArea).html(market.High1);
                                        $('.high1', viewArea).parent().addClass('change');
                                    }

                                    if ($('.close1', viewArea).html() != market.Close1) {
                                        $('.close1', viewArea).html(market.Close1);
                                        $('.close1', viewArea).parent().addClass('change');
                                    }

                                    if ($('.low2', viewArea).html() != market.Low2) {
                                        $('.low2', viewArea).html(market.Low2);
                                        $('.low2', viewArea).parent().addClass('change');
                                    }

                                    if ($('.high2', viewArea).html() != market.High2) {
                                        $('.high2', viewArea).html(market.High2);
                                        $('.high2', viewArea).parent().addClass('change');
                                    }

                                    if ($('.close2', viewArea).html() != market.Close2) {
                                        $('.close2', viewArea).html(market.Close2);
                                        $('.close2', viewArea).parent().addClass('change');
                                    }
                                }
                            }


                        });
                    }

                    var stockArea = null;
                    var viewArea = null;
                    if (data.Stocks.length > 0) {
                        var stockContainer = $('#stockArea');

                        $.each(data.Stocks, function (index, stock) {
                            stockArea = $('#stock' + stock.StockCode, stockContainer);
                            if (stockArea != undefined && stockArea != null) {
                                if ($('.price', stockArea).html() != stock.Price) {
                                    $('.price', stockArea).html(stock.Price);
                                    if (stock.UpDown == 1) {
                                        $('.price', stockArea).parent().addClass('changeUp');
                                    }
                                    else if (stock.UpDown == -1) {
                                        $('.price', stockArea).parent().addClass('changeDown');
                                    }
                                    else {
                                        $('.price', stockArea).parent().addClass('change');
                                    }
                                }

                                if ($('.upDown', stockArea).val() != stock.UpDown) {
                                    $('.upDown', stockArea).val(stock.UpDown);
                                    if (stock.UpDown == 1) {
                                        $('.price', stockArea).parent().addClass('changeUp');
                                        $('.price', stockArea).attr('class', 'greenArrow price');
                                    }
                                    else if (stock.UpDown == -1) {
                                        $('.price', stockArea).parent().addClass('changeDown');
                                        $('.price', stockArea).attr('class', 'redArrow price');
                                    }
                                    else {
                                        $('.price', stockArea).parent().addClass('change');
                                        $('.price', stockArea).attr('class', 'blueArrow price');
                                    }
                                }

                                if ($('.bestBuy', stockArea).html() != stock.BestBuy) {
                                    $('.bestBuy', stockArea).html(stock.BestBuy);
                                    $('.bestBuy', stockArea).parent().addClass('change');
                                }

                                if ($('.bestSell', stockArea).html() != stock.BestSell) {
                                    $('.bestSell', stockArea).html(stock.BestSell);
                                    $('.bestSell', stockArea).parent().addClass('change');
                                }

                                if ($('.changePercent', stockArea).html() != stock.ChangePercent) {
                                    $('.changePercent', stockArea).html(stock.ChangePercent);
                                    $('.changePercent', stockArea).parent().addClass('change');
                                }

                                if ($('.totalCount', stockArea).html() != stock.TotalCount) {
                                    $('.totalCount', stockArea).html(stock.TotalCount);
                                    $('.totalCount', stockArea).parent().addClass('change');
                                }

                                if ($('.volume', stockArea).html() != stock.Volume) {
                                    $('.volume', stockArea).html(stock.Volume);
                                    $('.volume', stockArea).parent().addClass('change');
                                }
                            }

                            //Hızlı görünüme de bakar
                            if (viewContainer != undefined && viewContainer.is(':visible')) {
                                viewArea = $('#view' + stock.StockCode, viewContainer);

                                if (viewArea != undefined && viewArea != null) {
                                    if ($('.price', viewArea).html() != stock.Price) {
                                        $('.price', viewArea).html(stock.Price);

                                        if (stock.UpDown == 1) {
                                            $('.price', viewArea).parent().addClass('changeUp');
                                            $('.price', viewArea).attr('class', 'greenArrow price');
                                        }
                                        else if (stock.UpDown == -1) {
                                            $('.price', viewArea).parent().addClass('changeDown');
                                            $('.price', viewArea).attr('class', 'redArrow price');
                                        }
                                        else {
                                            $('.price', viewArea).parent().addClass('change');
                                            $('.price', viewArea).attr('class', 'blueArrow price');
                                        }
                                    }

                                    if ($('.bestBuy', viewArea).html() != stock.BestBuy) {
                                        $('.bestBuy', viewArea).html(stock.BestBuy);
                                        $('.bestBuy', viewArea).parent().addClass('change');
                                    }

                                    if ($('.bestSell', viewArea).html() != stock.BestSell) {
                                        $('.bestSell', viewArea).html(stock.BestSell);
                                        $('.bestSell', viewArea).parent().addClass('change');
                                    }

                                    if ($('.changePercent', viewArea).html() != stock.ChangePercent) {
                                        $('.changePercent', viewArea).html(stock.ChangePercent);
                                        $('.changePercent', viewArea).parent().addClass('change');
                                    }

                                    if ($('.totalCount', viewArea).html() != stock.TotalCount) {
                                        $('.totalCount', viewArea).html(stock.TotalCount);
                                        $('.totalCount', viewArea).parent().addClass('change');
                                    }

                                    if ($('.volume', viewArea).html() != stock.Volume) {
                                        $('.volume', viewArea).html(stock.Volume);
                                        $('.volume', viewArea).parent().addClass('change');
                                    }
                                }
                            }

                        });


                    }
                    setTimeout("ClearChange()", 1000);
                }
            }
            liveLock = false;
        });
    }
}

function UpdateLiveTime() {
    if ($('#hourTime').val() != '') {
        liveHour = parseInt($('#hourTime').val());
        liveMin = parseInt($('#minTime').val());
        liveSec = parseInt($('#secTime').val());

        if (liveSec < 59) {
            liveSec = liveSec + 1;
        }
        else {
            liveSec = 0;
            if (liveMin < 59) {
                liveMin = liveMin + 1;
            }
            else {
                liveMin = 0;
                if (liveHour < 23) {
                    liveHour = liveHour + 1;
                }
                else {
                    liveHour = 0;
                }
            }
        }

        $('#hourTime').val(liveHour);
        $('#minTime').val(liveMin);
        $('#secTime').val(liveSec);


        if (liveSec < 10) {
            $('.clock .sec').html('0' + liveSec);
        }
        else {
            $('.clock .sec').html(liveSec);
        }

        if (liveMin < 10) {
            $('.clock .min').html('0' + liveMin);
        }
        else {
            $('.clock .min').html(liveMin);
        }

        if (liveHour < 10) {
            $('.clock .hour').html('0' + liveHour);
        }
        else {
            $('.clock .hour').html(liveHour);
        }
    }
}

function GetComments() {
    var lastId = $('#lastComment').val();
    var newsId = $('#commentNewsId').val();
    if (lastId == '') {
        lastId = 0;
    }
    $.ajax({
        url: '/services/comment/list',
        data: { newsId: newsId, lastCommentId: lastId },
        dataType: 'json',
        type: 'post'
    }).done(function (data) {
        if (data != '0' && data.length > 0) {
            if (lastId == 0) {
                $("#commentList").append('<span class="bigTitle">YORUMLAR</span>');
            }
            $.each(data, function (index, item) {
                $("#commentList").append('<div class="commentBox boxModelOne"><div class="captionContainer clearfix"><span class="title">' + item._Sender + '</span><div class="counts"><span class="likecount">' + item.Like + '</span><span class="cnt"><span class="like" title="Beğendim"></span></span><span class="dislikecount">' + item.Dislike + '</span><span class="cnt"><span class="dislike" title="Beğenmedim"></span></span><input type="hidden" class="com" value="' + item.ID + '"/></div></div><span class="text">' + item.Body + '</span></div><hr/>');
                if (index == (data.length - 1)) {
                    $('#lastComment').val(item.ID);
                }
            });

            $('.like').unbind('click');
            $('.like').click(function () {
                LikeComment($(this));
            });

            $('.dislike').unbind('click');
            $('.dislike').click(function () {
                DislikeComment($(this));
            });

            $('.complaint').unbind('click');
            $('.complaint').click(function () {
                ComplaintComment($(this));
            });

            if (data.length < 10) {
                $('#moreComment').hide(0);
            }
            else {
                $('#moreComment').show(0);
            }
        }
        else {

            $('#moreComment').hide(0);
        }

    });

}

function LikeComment(self) {
    var cnt = self.parent().parent();
    var id = $('.com', cnt).val();
    var newsId = $('#commentNewsId').val();
    if (id != undefined && id != null) {
        self.fadeOut('200', function () {
            $.ajax({
                url: '/services/comment/like',
                data: { commentId: id, newsId: newsId },
                dataType: 'json',
                type: 'post'
            }).done(function (data) {
                if (data == '1') {
                    alert("Yorum ile ilgili görüşünüz alındı. Teşekkürler.");
                    var count = parseInt($('.likecount', cnt).html());
                    $('.likecount', cnt).html((count + 1));
                }
                else if (data == '2') {
                    alert("Yorum hakkında daha önce görüş belirttiniz.");
                }
                else {
                    alert("İşleminiz tamamlanamadı! Lütfen tekrar deneyiniz.")
                }
                self.fadeIn('500');
            });
        });
    }
}

function DislikeComment(self) {
    var cnt = self.parent().parent();
    var id = $('.com', cnt).val();
    var newsId = $('#commentNewsId').val();
    if (id != undefined && id != null) {
        self.fadeOut('200', function () {
            $.ajax({
                url: '/services/comment/dislike',
                data: { commentId: id, newsId: newsId },
                dataType: 'json',
                type: 'post'
            }).done(function (data) {
                if (data == '1') {
                    alert("Yorum ile ilgili görüşünüz alındı. Teşekkürler.");
                    var count = parseInt($('.dislikecount', cnt).html());
                    $('.dislikecount', cnt).html((count + 1));
                }
                else if (data == '2') {
                    alert("Yorum hakkında daha önce görüş belirttiniz.");
                }
                else {
                    alert("İşleminiz tamamlanamadı! Lütfen tekrar deneyiniz.")
                }
                self.fadeIn('500');
            });
        });
    }
}

function ComplaintComment(self) {
    var cnt = self.parent().parent();
    var id = $('.com', cnt).val();
    if (id != undefined && id != null) {
        self.fadeOut('200', function () {
            $.ajax({
                url: '/services/comment/complaint',
                data: { commentId: id },
                dataType: 'json',
                type: 'post'
            }).done(function (data) {
                if (data == '1') {
                    alert("Şikayetiniz alındı. Teşekkürler.");
                }
                else {
                    alert("İşleminiz tamamlanamadı! Lütfen tekrar deneyiniz.")
                }
                self.fadeIn('500');
            });
        });
    }
}

function Adv() {
    $.ajax({
        url: '/services/ad',
        dataType: 'json',
        type: 'post'
    }).done(function (data) {
        if (data == "1") {
            $('.rightBanner').show(0);
            $('.adv').show(0);

        }
        else {
            $('.rightBanner').hide(0);
            $('.adv').hide(0);
        }

    });
}

function SetTargetBlank(container) {
    $.ajax({
        url: '/services/ad',
        dataType: 'json',
        type: 'post'
    }).done(function (data) {
        if (data == "1") {
            if (container == undefined || container == null) {
                $('a.target').attr("target", "_blank");

                $('#mainMenu .newTab a').attr("target", "_blank");
            }
            else {
                $('a.target', container).attr("target", "_blank");
            }
        }
        else {

        }

    });

}

function FocusOnPagePart(part) {
    $.scrollTo(parseInt($(part).position().top), 900);
}

function PrintNews(id) {
    window.open('/haber-yazdir/' + id);
}


function WatchListSearch(cnt) {
    if (!cnt.is('.refreshed')) {
        var group = cnt.attr('group');
        var selectText = $('option', cnt).eq(0).text();
        if (group != undefined && group != null) {
            $('option', cnt).eq(0).text('Yükleniyor...');
            $.ajax({
                url: '/services/watchlist/SearchItemNotAdded',
                data: { group: group },
                dataType: 'json',
                type: 'post'
            }).done(function (data) {
                $('option', cnt).remove();
                AddOption(cnt, '0', selectText, true);
                $('option', cnt).addClass('first');

                $.each(data, function (index, data) {
                    AddOption(cnt, data.Code, data.Title, false);
                });

                cnt.addClass('refreshed');
            });
        }
    }
}

function RefreshWatchListBox(group) {
    ShowWatchListLoader(group);
    $.ajax({
        url: '/services/watchlist/GetWatchListPartial',
        data: { group: group },
        dataType: 'html',
        type: 'post'
    }).done(function (data) {
        HideWatchListLoader(group);
        switch (parseInt(group)) {
            case 1:
                $('#marketContainer').html(data);
                break;
            case 2:
                $('#stockContainer').html(data);
                break;
            case 3:
                $('#commodityContainer').html(data);
                break;
            case 4:
                $('#bondsAndInterestsContainer').html(data);
                break;
            case 5:
                $('#currenciesContainer').html(data);
                break;
            case 6:
                $('#exchangeContainer').html(data);
                break;
            case 7:
                $('#fundContainer').html(data);
                break;
            case 8:
                $('#VOBContainer').html(data);
                break;
            case 9:
                $('#worldMarketsContainer').html(data);
                break;
            default:
                break;
        }
    });
}
function UpdateWatchListRightBoxPeriodic() {
    $('.boxArea', $('#watchRight')).each(function (box) {
        RefreshWatchListRightBox($(this).attr('type'));
    });

}
function ShowWatchListRightBoxLoader(type) {
    switch (parseInt(type)) {
        case 1:
            $('#centralBank .loader').show(0);
            break;
        case 2:
            $('#kapNews .loader').show(0);
            break;
        case 3:
            $('#marketPerformance .loader').show(0);
            break;
        case 4:
            $('#stockLastQuotes .loader').show(0);
            break;
        case 5:
            $('#relatedNews .loader').show(0);
            break;
        case 6:
            $('#lastNews .loader').show(0);
            break;
        default:
            break;
    }
}
function HideWatchListRightBoxLoader(type) {
    switch (parseInt(type)) {
        case 1:
            $('#centralBank .loader').hide(0);
            break;
        case 2:
            $('#kapNews .loader').hide(0);
            break;
        case 3:
            $('#marketPerformance .loader').hide(0);
            break;
        case 4:
            $('#stockLastQuotes .loader').hide(0);
            break;
        case 5:
            $('#relatedNews .loader').hide(0);
            break;
        case 6:
            $('#lastNews .loader').hide(0);
            break;
        default:
            break;
    }
}

function RefreshWatchListRightBox(type) {
    ShowWatchListRightBoxLoader(type);
    switch (parseInt(type)) {
        case 1:
            $.ajax({
                url: '/services/watchlist/CentralBankRight',
                dataType: 'html',
                type: 'get'
            }).done(function (data) {
                $('#centralBank').html(data);
                HideWatchListRightBoxLoader(type);
            });
            break;
        case 2:
            $.ajax({
                url: '/services/watchlist/KAPNewsRight',
                dataType: 'html',
                type: 'post'
            }).done(function (data) {
                $('#kapNews').html(data);
                HideWatchListRightBoxLoader(type);
            });
            break;
        case 3:
            $.ajax({
                url: '/services/watchlist/MarketPerformanceRight',
                dataType: 'html',
                type: 'post'
            }).done(function (data) {
                $('#marketPerformance').html(data);
                HideWatchListRightBoxLoader(type);
            });
            break;
        case 4:
            $.ajax({
                url: '/services/watchlist/StockLastQuotesRight',
                dataType: 'html',
                type: 'get'
            }).done(function (data) {
                $('#stockLastQuotes').html(data);
                HideWatchListRightBoxLoader(type);
            });
            break;
        case 5:
            $.ajax({
                url: '/services/watchlist/RelatedNewsRight',
                dataType: 'html',
                type: 'get'
            }).done(function (data) {
                $('#relatedNews').html(data);
                HideWatchListRightBoxLoader(type);
            });
            break;
        case 6:
            $.ajax({
                url: '/services/watchlist/LastNewsRight',
                dataType: 'html',
                type: 'get'
            }).done(function (data) {
                $('#lastNews').html(data);
                HideWatchListRightBoxLoader(type);
            });
            break;
        default:
            break;
    }

}
function AddWatchListBySelect(cnt) {
    var group = cnt.attr('group');
    var type = cnt.attr('type');
    var val = cnt.val();
    if (val != '0') {
        if (group != undefined && group != null) {
            ShowWatchListLoader(group);
            $.ajax({
                url: '/services/watchlist/add',
                data: { code: val, group: group, type: type },
                dataType: 'json',
                type: 'post'
            }).done(function (data) {
                HideWatchListLoader(group);
                if (data == '1') {
                    RefreshWatchListBox(group)
                    $("option:selected", cnt).remove();
                    cnt.removeClass('refreshed');
                } else {
                    alert('İşleminiz tamamlanamadı! Lütfen daha sonra tekrar deneyiniz.');
                }
            });

        }
    }
}

function RemoveWatchListBox(cnt) {
    var group = cnt.attr('group');
    var code = cnt.attr('code');

    if (group != undefined && group != null) {
        ShowWatchListLoader(group);
        $.ajax({
            url: '/services/watchlist/remove',
            data: { code: code, group: group },
            dataType: 'json',
            type: 'post'
        }).done(function (data) {
            HideWatchListLoader(group);
            if (data == '1') {
                RefreshWatchListBox(group);
                cnt.removeClass('refreshed');
            } else {
                alert('İşleminiz tamamlanamadı! Lütfen daha sonra tekrar deneyiniz.');
            }
        });


    }
}

function ChangeWatchListItemOrder(cnt) {
    var group = cnt.attr('group');
    var code = cnt.attr('code');
    var order = cnt.val();
    if (group != undefined && group != null && code != undefined && code != null) {
        ShowWatchListLoader(group);
        $.ajax({
            url: '/services/watchlist/ChangeItemOrder',
            data: { code: code, group: group, order: order },
            dataType: 'json',
            type: 'post'
        }).done(function (data) {
            HideWatchListLoader(group);
            if (data == '1') {
                RefreshWatchListBox(group);
            } else {
                alert('İşleminiz tamamlanamadı! Lütfen daha sonra tekrar deneyiniz.');
            }

        });


    }
}
function ChangeWatchListGroupOrder(cnt) {
    var group = cnt.attr('group');
    var order = cnt.val();
    if (group != undefined && group != null) {
        ShowWatchListLoader(group);
        $.ajax({
            url: '/services/watchlist/ChangeGroupOrder',
            data: { group: group, order: order },
            dataType: 'json',
            type: 'post'
        }).done(function (data) {
            if (data == '1') {

                window.document.location.href = window.document.location.href;
            } else {
                HideWatchListLoader(group);
                alert('İşleminiz tamamlanamadı! Lütfen daha sonra tekrar deneyiniz.');
            }

        });


    }
}
function ShowWatchListLoader(group) {
    switch (parseInt(group)) {
        case 1:
            $('#market .loading').show(0);
            break;
        case 2:
            $('#stock .loading').show(0);
            break;
        case 3:
            $('#commodity .loading').show(0);
            break;
        case 4:
            $('#interest .loading').show(0);
            break;
        case 5:
            $('#currency .loading').show(0);
            break;
        case 6:
            $('#exchange .loading').show(0);
            break;
        case 7:
            $('#fund .loading').show(0);
            break;
        case 8:
            $('#vob .loading').show(0);
            break;
        case 9:
            $('#worldMarket .loading').show(0);
            break;
        default:
            break;
    }
}

function HideWatchListLoader(group) {
    switch (parseInt(group)) {
        case 1:
            $('#market .loading').hide(0);
            break;
        case 2:
            $('#stock .loading').hide(0);
            break;
        case 3:
            $('#commodity .loading').hide(0);
            break;
        case 4:
            $('#interest .loading').hide(0);
            break;
        case 5:
            $('#currency .loading').hide(0);
            break;
        case 6:
            $('#exchange .loading').hide(0);
            break;
        case 7:
            $('#fund .loading').hide(0);
            break;
        case 8:
            $('#vob .loading').hide(0);
            break;
        case 9:
            $('#worldMarket .loading').hide(0);
            break;
        default:
            break;
    }
}

function UpdateWatchListPeriodic() {
    var group = '';
    $('.addWatchSelect').each(function () {
        group = $(this).attr('group');
        if (group != '1' && group != '2' && group != '6') {
            RefreshWatchListBox(group);
        }
    });
}

function ItemListIsAddedToWatchList(type, group, selector) {
    $.ajax({
        url: '/services/watchlist/GetTypeWatchList',
        data: { type: type },
        dataType: 'json',
        type: 'post'
    }).done(function (data) {
        if (data.length > 0) {
            var cont = null;
            $.each(data, function (index, data) {
                cont = $('span[code="' + data.FinanceCode + '"]');
                if (cont != undefined && cont != null) {
                    cont.removeClass('posiPlus');
                    cont.addClass('negaMinus');
                    cont.html('x');
                }
            });
        }

        $('.' + selector).click(function () {
            if ($(this).is('.posiPlus')) {
                AddWatchListItemList($(this), group, $(this).attr('code'), type);
            }
            else {
                RemoveWatchListItemList($(this), group, $(this).attr('code'));
            }
        });
    });
}

function AddWatchListItemList(cnt, group, code, type) {
    if (!isLogin) {
        OpenLoginBox();
    } else {

        if (code != undefined && code != null) {
            $.ajax({
                url: '/services/watchlist/add',
                data: { code: code, group: group, type: type },
                dataType: 'json',
                type: 'post'
            }).done(function (data) {
                if (data == '1') {
                    cnt.removeClass('posiPlus');
                    cnt.addClass('negaMinus');
                    cnt.html('x');
                }

            });
        }

    }
}

function RemoveWatchListItemList(cnt, group, code) {
    if (code != undefined && code != null) {
        $.ajax({
            url: '/services/watchlist/remove',
            data: { code: code, group: group },
            dataType: 'json',
            type: 'post'
        }).done(function (data) {
            if (data == '1') {
                cnt.addClass('posiPlus');
                cnt.removeClass('negaMinus');
                cnt.html('+');
            }
        });
    }
}

function UpdateWatchListLive() {

    if (watchListLiveLock) {
        watchListLiveLockCount = watchListLiveLockCount + 1;

        if (watchListLiveLockCount >= 4) {
            watchListLiveLock = false;
            watchListLiveLockCount = 0;
        }
        setTimeout("UpdateWatchListLive()", 1000);
    }
    else {
        watchListLiveLock = true;
        setTimeout("UpdateWatchListLive()", 3000);

        var code = 'XUTUM';
        $.ajax({
            url: '/services/finance/GetLiveMarket',
            data: { code: code },
            dataType: 'json',
            type: 'post'
        }).done(function (data) {
            if (data != null) {
                ShowWatchListLoader(1);
                ShowWatchListLoader(2);
                ShowWatchListLoader(6);
                if (data.Markets != undefined && data.Markets != null && data.Stocks != undefined && data.Stocks != null && data.Exchanges != undefined && data.Exchanges != null) {
                    var marketArea = null;

                    if (data.Markets.length > 0) {
                        var marketContainer = $('#market');
                        $.each(data.Markets, function (index, market) {
                            marketArea = $('#market' + market.MarketCode, marketContainer);
                            //Market
                            if (marketArea != undefined && marketArea != null) {
                                if ($('.price', marketArea).html() != market.Price) {

                                    $('.price', marketArea).html(market.Price);
                                    if (market.UpDown == 1) {
                                        $('.price', marketArea).parent().addClass('changeUp');
                                    }
                                    else if (market.UpDown == -1) {
                                        $('.price', marketArea).parent().addClass('changeDown');
                                    }
                                    else {
                                        $('.price', marketArea).parent().addClass('change');
                                    }
                                }

                                if ($('.upDown', marketArea).val() != market.UpDown) {
                                    $('.upDown', marketArea).val(market.UpDown);
                                    if (market.UpDown == 1) {
                                        $('.price', marketArea).parent().addClass('changeUp');
                                        $('.price', marketArea).attr('class', 'greenArrow price');
                                    }
                                    else if (market.UpDown == -1) {
                                        $('.price', marketArea).parent().addClass('changeDown');
                                        $('.price', marketArea).attr('class', 'redArrow price');
                                    }
                                    else {
                                        $('.price', marketArea).parent().addClass('change');
                                        $('.price', marketArea).attr('class', 'blueArrow price');
                                    }
                                }

                                if ($('.low', marketArea).html() != market.DayLow) {
                                    $('.low', marketArea).html(market.DayLow);
                                    $('.low', marketArea).parent().addClass('change');
                                }

                                if ($('.high', marketArea).html() != market.DayHigh) {
                                    $('.high', marketArea).html(market.DayHigh);
                                    $('.high', marketArea).parent().addClass('change');
                                }


                                $('.updated', marketArea).html(market.UpdatedOn.substring(0, 2) + ':' + market.UpdatedOn.substring(2, 4) + ':' + market.UpdatedOn.substring(4, 6));

                            }


                        });
                    }

                    var stockArea = null;

                    if (data.Stocks.length > 0) {
                        var stockContainer = $('#stock');

                        $.each(data.Stocks, function (index, stock) {
                            stockArea = $('#stock' + stock.StockCode, stockContainer);
                            if (stockArea != undefined && stockArea != null) {
                                if ($('.price', stockArea).html() != stock.Price) {
                                    $('.price', stockArea).html(stock.Price);
                                    if (stock.UpDown == 1) {
                                        $('.price', stockArea).parent().addClass('changeUp');
                                    }
                                    else if (stock.UpDown == -1) {
                                        $('.price', stockArea).parent().addClass('changeDown');
                                    }
                                    else {
                                        $('.price', stockArea).parent().addClass('change');
                                    }
                                }

                                if ($('.upDown', stockArea).val() != stock.UpDown) {
                                    $('.upDown', stockArea).val(stock.UpDown);
                                    if (stock.UpDown == 1) {
                                        $('.price', stockArea).parent().addClass('changeUp');
                                        $('.price', stockArea).attr('class', 'greenArrow price');
                                    }
                                    else if (stock.UpDown == -1) {
                                        $('.price', stockArea).parent().addClass('changeDown');
                                        $('.price', stockArea).attr('class', 'redArrow price');
                                    }
                                    else {
                                        $('.price', stockArea).parent().addClass('change');
                                        $('.price', stockArea).attr('class', 'blueArrow price');
                                    }
                                }

                                if ($('.bestBuy', stockArea).html() != stock.BestBuy) {
                                    $('.bestBuy', stockArea).html(stock.BestBuy);
                                    $('.bestBuy', stockArea).parent().addClass('change');
                                }

                                if ($('.bestSell', stockArea).html() != stock.BestSell) {
                                    $('.bestSell', stockArea).html(stock.BestSell);
                                    $('.bestSell', stockArea).parent().addClass('change');
                                }

                                if ($('.dayLow', stockArea).html() != stock.DayLow) {
                                    $('.dayLow', stockArea).html(stock.DayLow);
                                    $('.dayLow', stockArea).parent().addClass('change');
                                }

                                if ($('.dayHigh', stockArea).html() != stock.DayHigh) {
                                    $('.dayHigh', stockArea).html(stock.DayHigh);
                                    $('.dayHigh', stockArea).parent().addClass('change');
                                }

                                $('.updated', stockArea).html(stock.UpdatedOn.substring(0, 2) + ':' + stock.UpdatedOn.substring(2, 4) + ':' + stock.UpdatedOn.substring(4, 6));
                            }

                        });


                    }

                    var exchangeArea = null;

                    if (data.Exchanges.length > 0) {
                        var exchangeContainer = $('#exchange');
                        $.each(data.Exchanges, function (index, exchange) {

                            exchangeArea = $('#exchange' + exchange.Code.replace(/\//g, ""), exchangeContainer);

                            if (exchangeArea != undefined && exchangeArea != null) {
                                if ($('.rate', exchangeArea).html() != exchange.Rate) {

                                    $('.rate', exchangeArea).html(exchange.Rate);
                                    if (exchange.UpDown == 1) {
                                        $('.rate', exchangeArea).parent().addClass('changeUp');
                                    }
                                    else if (exchange.UpDown == -1) {
                                        $('.rate', exchangeArea).parent().addClass('changeDown');
                                    }
                                    else {
                                        $('.rate', exchangeArea).parent().addClass('change');
                                    }
                                }

                                if ($('.upDown', exchangeArea).val() != exchange.UpDown) {
                                    $('.upDown', exchangeArea).val(exchange.UpDown);
                                    if (exchange.UpDown == 1) {
                                        $('.rate', exchangeArea).parent().addClass('changeUp');
                                        $('.rate', exchangeArea).attr('class', 'greenArrow rate');
                                    }
                                    else if (exchange.UpDown == -1) {
                                        $('.rate', exchangeArea).parent().addClass('changeDown');
                                        $('.rate', exchangeArea).attr('class', 'redArrow rate');
                                    }
                                    else {
                                        $('.rate', exchangeArea).parent().addClass('change');
                                        $('.rate', exchangeArea).attr('class', 'blueArrow rate');
                                    }
                                }

                                if ($('.low', exchangeArea).html() != exchange.Low) {
                                    $('.low', exchangeArea).html(exchange.Low);
                                    $('.low', exchangeArea).parent().addClass('change');
                                }

                                if ($('.high', exchangeArea).html() != exchange.High) {
                                    $('.high', exchangeArea).html(exchange.High);
                                    $('.high', exchangeArea).parent().addClass('change');
                                }

                                if ($('.buy', exchangeArea).html() != exchange.Buy) {
                                    $('.buy', exchangeArea).html(exchange.Buy);
                                    $('.buy', exchangeArea).parent().addClass('change');
                                }

                                if ($('.sell', exchangeArea).html() != exchange.Sell) {
                                    $('.sell', exchangeArea).html(exchange.Sell);
                                    $('.sell', exchangeArea).parent().addClass('change');
                                }


                                $('.updated', exchangeArea).html(exchange.UpdatedOn.substring(0, 2) + ':' + exchange.UpdatedOn.substring(2, 4) + ':' + exchange.UpdatedOn.substring(4, 6));

                            }


                        });
                    }

                    setTimeout("ClearChangeWatchListLive()", 1000);

                }
            }
            liveLock = false;
        });
    }
}

function ClearChangeWatchListLive() {
    $('.price').parent().removeClass('changeUp');
    $('.price').parent().removeClass('changeDown');
    $('.price').parent().removeClass('change');

    $('.bestBuy').parent().removeClass('changeUp');
    $('.bestBuy').parent().removeClass('changeDown');
    $('.bestBuy').parent().removeClass('change');

    $('.bestSell').parent().removeClass('changeUp');
    $('.bestSell').parent().removeClass('changeDown');
    $('.bestSell').parent().removeClass('change');

    $('.dayLow').parent().removeClass('changeUp');
    $('.dayLow').parent().removeClass('changeDown');
    $('.dayLow').parent().removeClass('change');

    $('.dayHigh').parent().removeClass('changeUp');
    $('.dayHigh').parent().removeClass('changeDown');
    $('.dayHigh').parent().removeClass('change');

    $('.low').parent().removeClass('changeUp');
    $('.low').parent().removeClass('changeDown');
    $('.low').parent().removeClass('change');

    $('.high').parent().removeClass('changeUp');
    $('.high').parent().removeClass('changeDown');
    $('.high').parent().removeClass('change');

    $('.rate').parent().removeClass('changeUp');
    $('.rate').parent().removeClass('changeDown');
    $('.rate').parent().removeClass('change');

    $('.buy').parent().removeClass('changeUp');
    $('.buy').parent().removeClass('changeDown');
    $('.buy').parent().removeClass('change');

    $('.sell').parent().removeClass('changeUp');
    $('.sell').parent().removeClass('changeDown');
    $('.sell').parent().removeClass('change');

    HideWatchListLoader(1);
    HideWatchListLoader(2);
    HideWatchListLoader(6);
}

function Slide(i, p) {
    //var url = document.location.href;
    //var last = url.match(/\/[^\/]*$/, '/')[0].replace('/', '');
    //if (isNaN(parseInt(last))) {
    //    url = url + '/1';
    //}
    //else {
    //    url = url.replace(/\/[^\/]*$/, '/' + p);
    //}
    //window.location = url;
    //return;
    $.ajax({
        url: '/services/slide',
        data: { id: i, page: p },
        dataType: 'json',
        type: 'get'
    }).done(function (data) {
        if (!data.HasError) {
            $('#currentIndex').val(data.CurrentPage);
            $('#title').html(data.Title);
            $('#summary').html(data.Summary);
            //$('#date').html(data.PublishTime + ' - ' + data.Source);
            $('#date').html(data.Source);
            $('a', $('#photo')).attr('title', data.Title);
            $('img', $('#photo')).attr('src', data.ImageUrl);
            $('img', $('#photo')).attr('title', data.Title);
            $('img', $('#photo')).attr('alt', data.Title);

            if (parseInt(data.TotalPage) > 10 && (parseInt(data.CurrentPage) >= 5 || parseInt(data.CurrentPage) == 1)) {
                for (var i = 0; i <= (parseInt(data.LegandPageEnd) - parseInt(data.LegandPageStart)) ; i++) {
                    $('a.nav').eq(i).html(parseInt(data.LegandPageStart) + i);
                    $('a.nav').eq(i).attr('num', parseInt(data.LegandPageStart) + i);
                }
            }

            $('a.nav').parent().removeClass('selected');
            $('a.nav[num="' + data.CurrentPage + '"]').parent().addClass('selected');



            state = window.history.pushState !== undefined
            if (state == true) {
                history.pushState('', '', data.Url);
            }
            else {
                $.address.value(data.CurrentPage);
            }

            if (typeof _gaq != "undefined") {
                _gaq.push(['_trackPageview', data.Url]);
            }

            //reklam alanı güncelleniyor
            $('#advertisement200_250').html($('#advertisement200_250').html());
        }
    });

    UpdateSlideStatic();

}

function UpdateSlideStatic() {
    var id = $('#slideID').val();
    $.ajax({
        url: '/services/updateslidestatics',
        data: { id: id },
        dataType: 'json',
        type: 'post'
    });
}

function NextSlide() {
    var current = parseInt($('#currentIndex').val());
    var total = parseInt($('#totalPage').val());
    var id = $('#slideID').val();
    if (current >= total) {
        window.document.location.href = "/foto-galeri";
        return;
    }
    Slide(id, current + 1);
}

function PreviousSlide() {
    var current = parseInt($('#currentIndex').val());
    var total = parseInt($('#totalPage').val());
    var id = $('#slideID').val();
    if (current <= 1) {
        window.document.location.href = "/foto-galeri";
        return;
    }
    Slide(id, current - 1);
}

function GetSlide(p) {
    var total = parseInt($('#totalPage').val());
    var id = $('#slideID').val();
    if (parseInt(p) < 1 || parseInt(p) > total) {
        window.document.location.href = "/foto-galeri";
        return;
    }
    Slide(id, p);
}


function AddQuestion() {
    var name = $('#fromName').val();
    var surname = $('#fromSurname').val();
    var question = $('#question').val();
    var program = $('#program').val();

    if (TrimString(name) == '' || TrimString(surname) == '' || TrimString(question) == '' || program == '') {
        alert("Alanların tamamını doldurmalısınız!");
        return;
    }

    if (name.length > 50) {
        alert("Adınız alanına en fazla 50 karakter girebilirsiniz.");
        return
    }

    if (surname.length > 50) {
        alert("Soyadınız alanına en fazla 50 karakter girebilirsiniz.");
        return
    }

    if (question.length > 500) {
        alert("Sorunuz alanına en fazla 500 karakter girebilirsiniz.");
        return
    }

    var self = $('#saveQuestion');


    self.fadeOut('200', function () {
        $.ajax({
            url: '/services/livequestion/addquestion',
            data: { name: name, surname: surname, question: question, programID: program },
            dataType: 'json',
            type: 'post'
        }).done(function (data) {
            if (data == '1') {
                alert("Sorunuz alındı teşekkürler.")
            }
            else {
                alert("Sorunuz tamamlanamadı! Lütfen tekrar deneyiniz.")
            }
            self.fadeIn('500');
        });
    });

}

function GetLiveQuestion() {
    var program = $('#program').val();

    if (program != null && program != '') {

        var isLive = $('#programLiveStatus').val();

        $.ajax({
            url: '/services/livequestion/GetLiveQuestion',
            data: { programID: program },
            dataType: 'json',
            type: 'post'
        }).done(function (data) {
            if (data != null) {
                if (data.result.programIsLive != isLive) {
                    window.document.location.href = '/sorun';
                }
            }
        });

    }
}

function GetLiveScreenQuestion() {
    var program = $('#program').val();

    if (program != null && program != '') {

        var self = $('#currentQuestion');
        var code = $('#currentQuestionCode').val();

        $.ajax({
            url: '/services/livequestion/GetLiveQuestion',
            data: { programID: program },
            dataType: 'json',
            type: 'post'
        }).done(function (data) {
            if (data != null) {

                if (data.state == '1') {
                    if (code == '') {
                        $('#question', self).fadeOut('200', function () {
                            var length = data.result.question.length;
                            if (length > 230) {
                                length = 230;
                            }
                            if (length > 0 && length < data.result.question.length) {
                                $('#question', self).html(data.result.question.substring(0, length) + '...');
                            }
                            else {
                                $('#question', self).html(data.result.question);
                            }
                            $('#name', self).html(data.result.name);
                            $('#currentQuestionCode').val(data.result.ID);
                            $('#question', self).fadeIn('500');
                        });
                    }
                    else {

                        if (code != data.result.ID) {
                            $('#question', self).fadeOut('200', function () {
                                var length = data.result.question.length;
                                if (length > 230) {
                                    length = 230;
                                }
                                if (length > 0 && length < data.result.question.length) {
                                    $('#question', self).html(data.result.question.substring(0, length) + '...');
                                }
                                else {
                                    $('#question', self).html(data.result.question);
                                }

                                $('#name', self).html(data.result.name);
                                $('#currentQuestionCode').val(data.result.ID);
                                $('#question', self).fadeIn('500');
                            });
                        }

                    }

                }
            }
        });

    }
}

function CreateCookie(name, value, minutes) {
    if (minutes) {
        var date = new Date();
        date.setTime(date.getTime() + (minutes * 60 * 1000));
        var expires = "; expires=" + date.toGMTString();
    }
    else var expires = "";
    document.cookie = name + "=" + value + expires + "; path=/";
}
function GetCookie(c_name) {
    if (document.cookie.length > 0) {
        c_start = document.cookie.indexOf(c_name + "=");
        if (c_start != -1) {
            c_start = c_start + c_name.length + 1;
            c_end = document.cookie.indexOf(";", c_start);
            if (c_end == -1) {
                c_end = document.cookie.length;
            }
            return unescape(document.cookie.substring(c_start, c_end));
        }
    }
    return "";
}

function AskSaxoCapitalByEmail() {
    var name = $('#mailName').val();
    var email = $('#mailEmail').val();
    var note = $('#mailNote').val();
    var phone = $('#mailPhone').val();


    if (name == null || name == '' || email == null || email == '' || note == null || note == '') {
        alert("Tüm alanları doldurmalısınız.");
        return;
    }

    $.ajax({
        url: '/services/email/asksaxocapital',
        data: {
            name: name,
            mail: email,
            note: note,
            phone: phone
        },
        dataType: 'json',
        type: 'post'
    }).done(function (data) {
        if (data == "1") {
            alert("İsteğiniz uzmanımıza iletildi. Teşekkürler.");
            CloseSendNewsMailBox();
        }
        else {
            alert("İsteğiniz iletilemedi! Lütfen daha sonra tekrar deneyiniz.");
        }

    });
}

