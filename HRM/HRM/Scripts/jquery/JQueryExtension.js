

//re-set all client validation given a jQuery selected form or child
$.fn.resetValidation = function () {
    var $form = this.closest('form');
    //reset unobtrusive field level, if it exists
    $form.find("[data-valmsg-replace]")
        .removeClass("field-validation-error")
        .addClass("field-validation-valid")
        .empty();

    $form.find("[data-val]")
        .removeClass("input-validation-error");

    return $form;
};

//reset a form given a jQuery selected form or a child
//by default validation is also reset
$.fn.formReset = function (resetValidation) {
    var $form = this.closest('form');

    //set boostrap checkbox span if not bind at popup open time
    $form.find('input[type=checkbox],input[type=radio]').each(function () {
        if (!$(this).next().hasClass("check")) {
            $(this).after('<span class="check" />');
        }
    });

    $form.find("[data-tooltip-type='help']").show();
    $form.find("[data-tooltip-type='error']").hide();
    $form.find("span.field-validation-error").removeAttr("style");

    $form.removeData('validator');
    $form.removeData('unobtrusiveValidation');
    $.validator.unobtrusive.parse($form);
    //$form[0].reset();

    if (resetValidation == undefined || resetValidation) {
        $form.resetValidation();
    }

    //$form.toolTipInit();
    //$form.errorToolTipInit();

    $form.on("mouseover focus", ".input-validation-error", function () {
        var name = $(this).attr("name");
        $("span.field-validation-error[data-valmsg-for='" + name + "']").attr("style", "display:block");
    });
    $form.on("mouseout blur", ".input-validation-error", function () {
        if ($(this).is(":focus") == false) {
            var name = $(this).attr("name");
            $("span.field-validation-error[data-valmsg-for='" + name + "']").removeAttr("style");
        }
    });

    $form.find('input[type="text"],select,textarea').first().focus();


    return $form;
}

//Set Focus of Control
$.fn.setFocus = function () {
    var input = $(this);
    input.focus();
};

//Fill Dropdown
$.fn.FillDropdown = function (list) {
    var dropdown = $(this);
    var selectedValue = $("input[id$=" + dropdown.attr('id') + "]").val();
    dropdown.empty();
    for (var i = 0; i < list.length; i++) {
        if (parseInt(list[i].Value) === parseInt(selectedValue))
            dropdown.append("<option value=" + list[i].Value + " selected='selected'>" + list[i].Text + "</option>");
        else
            dropdown.append("<option value=" + list[i].Value + ">" + list[i].Text + "</option>");
    }
    dropdown.setFocus();
};

//Reset Dropdown and Set Default 
$.fn.resetDropdown = function () {
    var dropdown = $(this);
    dropdown.empty();
    dropdown.append("<option>Select</option>");
};

//Validation of Email Address
$.fn.IsValidEmail = function () {

    var mailformat = /^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/;

    if ($(this).val().match(mailformat)) {
        return true;
    }
    else {
        return false;
    }
};

$.fn.SetUpCheckBox = function (e) {
    var checkBox = $(this);
    var checkBoxAll = $('input[name=' + this.attr('name') + 'All]')
    if (typeof checkBoxAll !== "undefined") {

        checkBoxAll.on('ifChecked', function (e) {
            checkBox.iCheck('check');
        });

        checkBoxAll.on('ifUnchecked', function (e) {
            if (checkBox.filter(':checked').length == checkBox.length)
                checkBox.iCheck('uncheck');
        });

        checkBox.on('ifChanged', function (e) {
            if (checkBox.filter(':checked').length == checkBox.length) {
                checkBoxAll.iCheck('check');
            }
            else {
                checkBoxAll.iCheck('uncheck');
            }
        });
    }
};

$.fn.GetCheckBoxValues = function (e) {
    var list = "";
    var checkBoxAll = $('input[name=' + this.attr('name') + 'All]');

    if (checkBoxAll.prop('checked'))
        return "all";


    $(this).filter(':checked').each(function (e) {

        if (list.length == 0) {
            list = this.value ;
        }
        else {
            list +=  "," + this.value ;
        }
    });
    return list;
};



// Table Multi Select Check box Methods

$.fn.TableMultiSelectCheckbox = function () {
    var $table = $(this);
    var triggeredByChild = false;

    // Initilize ICheckBox
    $('.flat').iCheck(
        {
            checkboxClass: 'icheckbox_flat-green',
            radioClass: 'iradio_flat-green'
        });

    var $headerCheckBox = $table.find('thead th input[type="checkbox"]');
    var $childCheckBox = $table.find('tbody td input[type="checkbox"]');

    $headerCheckBox.on('ifChecked', function (event) {
        $childCheckBox.iCheck('check');
        triggeredByChild = false;
    });

    $headerCheckBox.on('ifUnchecked', function (event) {
        if (!triggeredByChild) {
            $childCheckBox.iCheck('uncheck');
        }
        triggeredByChild = false;
    });


    // Removed the checked state from "All" if any checkbox is unchecked
    $childCheckBox.on('ifUnchecked', function (event) {
        triggeredByChild = true;
        $headerCheckBox.iCheck('uncheck');

        $(this).parent().parent().parent().removeClass('selected');
    });

    $childCheckBox.on('ifChecked', function (event) {

        $(this).parent().parent().parent().addClass('selected');

        if ($childCheckBox.filter(':checked').length == $childCheckBox.length) {
            $headerCheckBox.iCheck('check');
        }
    });
}


$.fn.TableCheckboxSelectWithShiftKey = function () {
    var $table = $(this);
    var lastChecked = null;
    $table.find('.iCheck-helper').on('click', function (event) {
        if (!lastChecked) {
            lastChecked = this;
            return;
        }
        if (event.shiftKey) {
            var start = $table.find('.iCheck-helper').index(this);
            var end = $table.find('.iCheck-helper').index(lastChecked);
            for (i = Math.min(start, end); i <= Math.max(start, end); i++) {
                $($table.find('input[type="checkbox"]')[i]).iCheck('check');
            }
        }
        lastChecked = this;
    });
};

$.fn.ToParseDate = function () {
    var date = this.replace(/[^0-9 +]/g, '');
    return new Date(parseInt(date));
};



//************************************************** Custom Date Control ************************************************** //




//$(function () {
//    // Replace the builtin US date validation with UK date validation
//    $.validator.addMethod(
//        "date",
//        function (value, element) {
//            var bits = value.match(/([0-9]+)/gi), str;
//            if (!bits)
//                return this.optional(element) || false;
//            str = bits[1] + '/' + bits[0] + '/' + bits[2];
//            return this.optional(element) || !/Invalid|NaN/.test(new Date(str));
//        },
//        "Please enter a date in the format dd/mm/yyyy"
//    );
//});