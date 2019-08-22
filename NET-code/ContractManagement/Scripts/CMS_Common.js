function textboxMultilineMaxNumber(txt, maxLen) {
                     if (txt.value.length > (maxLen - 1)) return false;           
        }
        
function DropdownValidation(source, arguments) {
    if (arguments.Value != "0") {
        arguments.IsValid = true;
    }
    else { arguments.IsValid = false; }
}


function RequireOtherTextIas(source, arguments) {
    arguments.IsValid = true;
//    var dd = document.getElementById('DdlType');
//    var tb = document.getElementById('TxtDescription');
//    var sel = dd.selectedIndex;
//    if (sel != "0") {
//        var seltext = dd.options[sel].value;
//        if ((seltext == "4") || (seltext == "5")) {
//            if (tb.value.length <= 0) {
//                args.IsValid = false;

//            } else { args.IsValid = true; }
//        }
//    } else { args.IsValid = true; }
}


