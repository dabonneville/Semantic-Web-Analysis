$(function () {
    $.support.cors = true;
    var $techDiary = $('#techDiary');
    if ($techDiary.length > 0) {
        $.ajax({
            url: "http://fotogaleri.ntvmsnbc.com/Rss/PhotoGalleryHandler.ashx",
            dataType: 'json'
        }).done(function (data) {
            if (data) {
                var item = data[0];
                if (item) {
                    var imgDir = 'http://fotogaleri.ntvmsnbc.com/Assets/PhotoGallery/Thumb/Workbench/tn210/';
                    var $content = $techDiary.find('.content');
                    var $head = $content.find('.head');
                    var $img = $techDiary.find('.img-o img');
                    var $description = $content.find('.description');
                    var title = item.Title;
                    var description = item.Description;
                    var picture = item.PictureName;
                    $head.text(title);
                    $description.text(description);
                    $img.attr('src', imgDir + picture);
                    $techDiary.show();
                }
            }
        }).fail(function (a, b, c) {
       
        });
    }
});