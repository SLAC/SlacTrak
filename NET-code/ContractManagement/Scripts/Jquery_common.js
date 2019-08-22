function OpenJQueryDialog(divid, ctrlid) {
  
            $('#' + divid).dialog({ autoOpen: false, title: "Select Owner", bgiframe: true, modal: true, width: 670, height: 470 });

            $('#' + divid).dialog('open');


            $('#' + divid).parent().appendTo($("form:first"));

            if (divid == "dialogowneradmin") {
                link = "../NameFinder.aspx";
            }
            else {
                link = 'NameFinder.aspx';
            }
           
            $('#modal' + divid).attr('src', link + '?field=' + ctrlid + '&dialog=' + divid);
            return false;
        }

        function OpenJQueryDialogSO(divid, ctrlid, isso) {
            $('#' + divid).dialog({ autoOpen: false, title: "Select Subowner", bgiframe: true, modal: true, width: 670, height: 470 });
            $('#' + divid).dialog('open');
            $('#' + divid).parent().appendTo($("form:first"));          
             link = 'NameFinder.aspx';          
            $('#modal' + divid).attr('src', link + '?field=' + ctrlid + '&dialog=' + divid + '&isso=y');
            return false;
        }

        function OpenJQueryFileDialog(divid, objid) {
            $('#' + divid).dialog({
                closeOnEscape: false,
                open: function(event, ui) {$(".ui-dialog-titlebar-close", ui.dialog).hide();},                
                autoOpen: false,
                title: "Attach Files",
                bgiframe: true,
                modal: true,
                width: 500,
                height: 350   //450             
                });

            $('#' + divid).dialog('open');


            $('#' + divid).parent().appendTo($("form:first"));

            link = 'Fileattach.aspx';

            $('#modal' + divid).attr('src', link + '?id=' + objid);
            return false;
        }


       
        function JQueryClose(divid) {

          window.parent.$('#' + divid).dialog('close');   
            return false;
        }

        function onKeypress(btnid) {
            if ((event.which && event.which == 13) || (event.keyCode && event.keyCode == 13)) {
                $('#' + btnid).click();

                return false;
            }
            else
                return true;
        }

        function OpenConfirmDialog(divid) {
            PrevDefault(event);

            $("#" + divid).dialog('open');
          
        }


        function PrevDefault(e) {
            if (e.preventDefault)
                e.preventDefault();
            else
                e.returnValue = false;

        }

       
        function ValidateCheckBoxList(sender, args) {
            args.IsValid = false;

                $("[id*=ChkFY] input:checked").each(function () {
            if (jQuery(this).attr("checked")) {

            args.IsValid = true;
            return;
                }
            });
        }



      
