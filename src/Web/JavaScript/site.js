/// <reference path="../Scripts/jquery-1.7.1-vsdoc.js"/>

/// Delete Link jQuery Stuff
$(function () {
    $('a[delete-link="true"]').click(function () {
        var $link = $(this);
        var performDelete = true;
        var confirmMessage = $link.attr('delete-link-confirm');
        if (confirmMessage)
            performDelete = confirm(confirmMessage);
        if (performDelete) {
            $.ajax({
                url: this.href,
                type: $link.attr('delete-link-method'),
                success: function () {
                    var parents = $link.attr('delete-link-num-parents');
                    var $removeTarget = $link;
                    for (var i = 0; i < parents; i++) {
                        $removeTarget = $removeTarget.parent();
                    }
                    $removeTarget.remove();
                }
            });
        }
        return false;
    });
});