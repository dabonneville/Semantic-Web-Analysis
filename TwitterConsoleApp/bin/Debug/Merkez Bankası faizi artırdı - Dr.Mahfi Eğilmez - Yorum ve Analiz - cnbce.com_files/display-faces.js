$(document).ready(function () {
    $(".follow-twitter").click(function () {
        window.open("https://twitter.com/intent/user?screen_name=" + $(this).attr("data-twitterId"), "twitterWindow", "menubar=0,resizable=1,width=550,height=420");
    });
});