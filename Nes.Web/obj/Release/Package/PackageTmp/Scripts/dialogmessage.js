function SetDialog() {
    try {
        if (_errPostback != undefined) {
            return;
        }
    }
    catch (ex) {
    }

    $('#hidDialogConfirm').val(1);
    $(function () {
        $('.InsertDialog, .UpdateDialog, .DeleteDialog, .ClearInput').click(function (e) {
            ShowConfirmDialog(e, this);
        });

        $("a").click(function (e) {
            if ($($(this).attr('href') != null && this).attr('href').indexOf('Page$') != -1) {
                ShowConfirmDialog(e, this);
            }
        });

        $('.ButtonDialog').click(function (e) {
            $('#hidDialogBtnName').val($(this).attr('name'));
        });

        // 2014/02/25 CuongLV ADD
        $('.LinkButtonDialog').click(function (e) {
            $('#hidDialogBtnName').val($(this).attr('id'));
        });

        function ShowConfirmDialog(e, thisObj) {
            if ($('#hidDialogConfirm').val() != '0') {

                if ($(thisObj).is('a')) {
                    $('#hidDialogBtnName').val($(thisObj).attr('href'));
                }
                else {
                    $('#hidDialogBtnName').val($(thisObj).attr('name'));
                }

                var IsDefaultMessage = true;

                if ($(thisObj).hasClass('DateCheckSatei') || $(thisObj).hasClass('DateCheckHanbai')) {
                    IsDefaultMessage = DateCheckHanbaiSatei();
                } else if ($(thisObj).hasClass('DateCheckKani')) {
                    IsDefaultMessage = DateCheckKani();
                } else if ($(thisObj).hasClass('DeleteTempo')) {
                    IsDefaultMessage = confirmMessage();
                }

                if (IsDefaultMessage == true) {

                    if ($(thisObj).hasClass('InsertDialog')) {
                        e.preventDefault();
                        if (!CheckInput()) {
                            return;
                        }
                        $('#dispMessage p').html('登録します。よろしいですか？');
                    } else if ($(thisObj).hasClass('UpdateDialog')) {
                        e.preventDefault();
                        if (!CheckInput()) {
                            return;
                        }
                        $('#dispMessage p').html('更新します。よろしいですか？');
                    } else if ($(thisObj).hasClass('DeleteDialog')) {
                        e.preventDefault();
                        $('#dispMessage p').html('削除します。<br/>一度削除すると元に戻せません。<br/>よろしいですか？');
                    } else if ($(thisObj).hasClass('ClearInput')
								|| ($(thisObj).is('a') && $(thisObj).attr('href').indexOf('Page$') != -1)) {
                        var hasGap = false;
                        try {
                            hasGap = HasGap();
                            if (!hasGap) {
                                var areaName = $('#hidAreaName').val();
                                hasGap = HasGapArea(areaName);
                            }
                        }
                        catch (exp) {
                            hasGap = false;
                        }
                        //hasGap = true;
                        if (!hasGap) {
                            if ($(thisObj).hasClass('ButtonBack')) {
                                try {
                                    BackToPreviousPage()
                                    e.preventDefault();
                                }
                                catch (e) {
                                }
                            }
                            return;
                        }
                        e.preventDefault();
                        $('#dispMessage p').html('変更したデータが破棄されます。<br/>よろしいですか？');
                    } else {
                        e.preventDefault();
                        return;
                    }
                }

                $('#ContactCenterDialog').dialog('open');
            } else {
                $('#dispMessage p').text('');
                $('#hidDialogConfirm').val('1');
                $('#hidDialogBtnName').val('');
            }
        }

        $("#ContactCenterDialog").dialog({
            autoOpen: false,
            closeOnEscape: false,
            modal: true,
            width: 400,
            open: function () {
                //				$('#footer2').hide();
                DialogDesign('footer', 'dispMessage');
            },
            buttons: {
                "OK": function () {
                    $('#dispMessage p').text('');
                    $('#hidDialogConfirm').val('0');
                    $(this).dialog('close');
                    if ($('#hidDialogBtnName').val().indexOf('Page$') != -1) {
                        $('#hidDialogConfirm').val('1');
                        eval($('#hidDialogBtnName').val());
                    }
                    else if ($("input[name='" + $('#hidDialogBtnName').val() + "']").hasClass('ButtonBack')) {
                        try {
                            BackToPreviousPage();
                        }
                        catch (e) {
                        }
                    }
                    else {
                        $("input[name='" + $('#hidDialogBtnName').val() + "']").trigger('click');
                    }
                },
                "キャンセル": function () {
                    $('#dispMessage p').text('');
                    $('#hidDialogConfirm').val('1');
                    $('#hidDialogBtnName').val('');
                    try {
                        var tab = $find($('.ajax__tab_container').prop('id'));
                        if (tab.get_activeTabIndex() != tab._cachedActiveTabIndex) {
                            tab.set_activeTabIndex(tab._cachedActiveTabIndex);
                        }
                    }
                    catch (e) {
                    }
                    $(this).dialog().dialog('close');
                }
            }
        });
    });
}

// 確認メッセージ用HTML
function DialogHtml() {
    document.write('	<div id="ContactCenterDialog" style="display:none;"> ');
    document.write('		<div class="MenuContainer"> ');
    document.write('			<table class="Master" style="width: 100%;"> ');
    document.write('				<tr> ');
    document.write('					<td> ');
    document.write('						<div class="PageTitle"> ');
    document.write('							<img id="Img1" alt="アイコン" src="../img/icon_h2_green.gif" /> ');
    document.write('							<sup> ');
    document.write('								<label id="Label1">確認メッセージ</label> ');
    document.write('							</sup> ');
    document.write('						</div> ');
    document.write('					</td> ');
    document.write('				</tr> ');
    document.write('			</table> ');
    document.write('		</div> ');
    document.write(' ');
    document.write('        <div id="dispMessage"><p></p></div> ');
    document.write('        <div id="deleteKbn" style="display: none;"><p>');
    document.write('        <input type="radio" name="q1" value="0" checked> 今回のみ<br/>');
    document.write('        <input type="radio" name="q1" value="1"> 以降すべて<br/>');
    document.write('        <input type="radio" name="q1" value="2"> 一連の定期的な予定すべて');
    document.write('        </p></div> ');
    document.write(' ');
    //	document.write('		<div class="Footer" id="footer"> ');
    //	document.write('			(c)&nbsp;2013&nbsp;Gulliver&nbsp;International&nbsp;Co.LTD&nbsp;All&nbsp;rights&nbsp;reserved. ');
    //	document.write('		</div> ');
    document.write('	</div> ');
}
function MessageDialog(msg) {
    //window.scroll(0, 0);
    $('#dispMessage2 p').html(msg);

    $("#messageDialog").dialog({
        autoOpen: true,
        closeOnEscape: false,
        modal: true,
        width: 400,
        open: function () {
            //			$('#footer2').show();
            DialogDesign('footer2', 'dispMessage2');
        },
        buttons: {
            "OK": function () {
                $(this).dialog('destroy');
                $('#dispMessage2 p').text('');
                $(this).dialog().dialog('close');

            }
        }
    });
}

function MessageDialogInformThenDoAction(msg, javascriptFunctionName) {
    //window.scroll(0, 0);
    $('#dispMessage2 p').html(msg);

    $("#messageDialog").dialog({
        autoOpen: true,
        closeOnEscape: false,
        modal: true,
        width: 400,
        open: function () {
            DialogDesign('footer2', 'dispMessage2');
        },
        buttons: {
            "OK": function () {
                $(this).dialog('destroy');
                $('#dispMessage2 p').text('');
                $(this).dialog().dialog('close');
                try {
                    if (javascriptFunctionName == null || javascriptFunctionName == '') {
                        javascriptFunctionName = "RunAfterInformOkClick()";
                    }
                    eval(javascriptFunctionName);
                }
                catch (e) {
                }
            }
        }
    });
}

function MessageDialogConfirmThenDoAction(msg, javascriptFunctionName) {
    //window.scroll(0, 0);
    $('#dispMessage2 p').html(msg);

    $("#messageDialog").dialog({
        autoOpen: true,
        closeOnEscape: false,
        modal: true,
        width: 400,
        open: function () {
            DialogDesign('footer2', 'dispMessage2');
        },
        buttons: {
            "OK": function () {
                $(this).dialog('destroy');
                $('#dispMessage2 p').text('');
                $(this).dialog().dialog('close');
                try {
                    if (javascriptFunctionName == null || javascriptFunctionName == '') {
                        javascriptFunctionName = "RunAfterInformOkClick()";
                    }
                    eval(javascriptFunctionName);
                }
                catch (e) {
                }
            },
            "キャンセル": function () {
                $('#dispMessage2 p').text('');
                $('#hidDialogConfirm').val('1');
                $('#hidDialogBtnName').val('');
                $(this).dialog().dialog('close');
            }
        }
    });
}

function MessageDialogConfirm(msg) {
    $('#dispMessage2 p').html(msg);

    $("#messageDialog").dialog({
        autoOpen: true,
        closeOnEscape: false,
        modal: true,
        width: 400,
        open: function () {
            //			$('#footer2').show();
            DialogDesign('footer2', 'dispMessage2');
        },
        buttons: {
            "OK": function () {
                $(this).dialog('destroy');
                $('#dispMessage2 p').text('');
                $('#hidDialogConfirm').val('0');
                //$(this).dialog().dialog('close');
                if ($('#hfConfirmSequence').val() != null && $('#hfConfirmSequence').val() != undefined) {
                    if (parseInt($('#hfConfirmSequence').val()) != NaN && parseInt($('#hfConfirmSequence').val()) != undefined) {
                        var sequence = parseInt($('#hfConfirmSequence').val()) + 1;
                        $('#hfConfirmSequence').val(sequence);
                    }
                }
                $("input[name='" + $('#hidDialogBtnName').val() + "']").trigger('click');

                // 2014/02/25 CuongLV ADD
                var ctl = $("a[id='" + $('#hidDialogBtnName').val() + "']");
                if (ctl.val() != undefined) {
                    var href = ctl.attr('href');
                    window.location.href = href;
                }
            },
            "キャンセル": function () {
                $('#dispMessage2 p').text('');
                $('#hidDialogConfirm').val('1');
                $('#hidDialogBtnName').val('');
                $(this).dialog().dialog('close');
            }
        }
    });
}

// Added by Nam to enable show 2 seperated message in a function (26/2/2014)
function MessageDialogConfirm1(msg) {
    $('#dispMessage2 p').html(msg);

    $("#messageDialog").dialog({
        autoOpen: true,
        closeOnEscape: false,
        modal: true,
        width: 400,
        open: function () {
            //			$('#footer2').show();
            DialogDesign('footer2', 'dispMessage2');
        },
        buttons: {
            "OK": function () {
                $(this).dialog('destroy');
                $('#dispMessage2 p').text('');
                $('#hidDialogConfirm1').val('0');
                $(this).dialog().dialog('close');
                if ($('#hfConfirmSequence') != null) {
                    if (parseInt($('#hfConfirmSequence').val()) != NaN) {
                        var sequence = parseInt($('#hfConfirmSequence').val()) + 1;
                        $('#hfConfirmSequence').val(sequence);
                    }
                }
                $("input[name='" + $('#hidDialogBtnName').val() + "']").trigger('click');
            },
            "キャンセル": function () {
                $('#dispMessage2 p').text('');
                $('#hidDialogConfirm1').val('1');
                $('#hidDialogBtnName').val('');
                $(this).dialog().dialog('close');
            }
        }
    });
}

function MessageDialogConfirmThenShowPopup(msg, pageParam, pageName) {
    $('#dispMessage2 p').html(msg);
    $("#messageDialog").dialog({
        autoOpen: true,
        closeOnEscape: false,
        modal: true,
        width: 400,
        open: function () {
            //			$('#footer2').show();
            DialogDesign('footer2', 'dispMessage2');
        },
        buttons: {
            "OK": function (e) {
                $(this).dialog('destroy');
                $('#dispMessage2 p').text('');
                $('#hidDialogConfirm').val('0');
                $(this).dialog().dialog('close');
                var param = '?' + pageParam.toString().replace(/"/gi, '').replace(/,/gi, '&&').replace(/:/gi, '=').replace(/{/gi, '').replace(/}/gi, '');
                var page = "../Pages/" + pageName + param;
                var wHeight = $(window).height();
                var dHeight = wHeight * 0.5;
                var dHeight1 = wHeight * 0.4;
                var $dialog = $('<div id="ContainerReception" ></div>')
				.html('<iframe id = "frameID" style="border: 0px; "' + 'scrolling="no"' + 'src="' + page + '" width="520px" height="' + dHeight1 + '"></iframe>')
				.dialog({
				    autoOpen: false,
				    modal: true,
				    height: dHeight,
				    width: 550,
				    title: "媒体選択",
				    close: function (event, ui) {
				        CallEvenButtonClick(event, ui);
				    }
				});
                $dialog.dialog('open');
                e.preventDefault();
            },
            "キャンセル": function () {
                $('#dispMessage2 p').text('');
                $('#hidDialogConfirm').val('1');
                $('#hidDialogBtnName').val('');
                $(this).dialog().dialog('close');
            }
        }
    });
}

// 完了メッセージ用HTML
function MessageDialogHtml() {
    document.write('	<div id="messageDialog" style="display:none;"> ');
    document.write('		<div class="MenuContainer"> ');
    document.write('			<table class="Master" style="width: 100%;"> ');
    document.write('				<tr> ');
    document.write('					<td> ');
    document.write('						<div class="PageTitle"> ');
    document.write('							<img id="Img2" alt="アイコン" src="../img/icon_h2_green.gif" /> ');
    document.write('							<sup> ');
    document.write('								<label id="Label2">確認メッセージ</label> ');
    document.write('							</sup> ');
    document.write('						</div> ');
    document.write('					</td> ');
    document.write('				</tr> ');
    document.write('			</table> ');
    document.write('		</div> ');
    document.write(' ');
    document.write('        <div id="dispMessage2"><p></p></div> ');
    document.write(' ');
    //	document.write('		<div class="Footer" id="footer2"> ');
    //	document.write('			(c)&nbsp;2013&nbsp;Gulliver&nbsp;International&nbsp;Co.LTD&nbsp;All&nbsp;rights&nbsp;reserved. ');
    //	document.write('		</div> ');
    document.write('	</div> ');
}

// ダイアログのデザイン(余計な装飾を取り除く)
function DialogDesign(footerId, dispMessageId) {
    // 画面全体
    $('.ui-widget-content').css('border', '0').css('background', 'white').css('color', 'black');
    // タイトルの背景色
    $('.ui-dialog-title').css('background-color', 'white');
    // タイトルバー
    $('.ui-dialog-titlebar').hide();
    // 内容
    $('.ui-dialog-content').css('background-color', 'white');
    // デフォルトフォントサイズ
    $('.ui-dialog-widget').css('font-size', '');
    // ボタン範囲
    $('.ui-dialog-buttonset').css('float', 'none');
    // ボタンパネル
    $('.ui-dialog-buttonpane').css('text-align', 'center');
    // ボタンサイズ・位置
    $('.ui-dialog-buttonset button').css('width', '120').css('margin', '10px');
    $('.ui-resizable-handle').css('display', 'none');
    // フッター
    //	footerId = "#" + footerId;
    //	$(footerId).appendTo('.ui-dialog');
    // メッセージ文字の装飾
    dispMessageId = "#" + dispMessageId + " p";
    $(dispMessageId).css({ 'font-weight': 'bold', 'font-size': '17px', 'text-align': 'center', 'padding-top': '20px' });
}

// 日付チェック 案件詳細(販売)＆案件詳細(査定)
function DateCheckHanbaiSatei() {
    //チェック用日数の取得
    Icount = document.getElementById("hidDate").value;
    //画面の日付の取得
    txtData = document.getElementById("txtDate").value;
    var today = new Date();
    var setDate = new Date(txtData);
    myseq = setDate.getTime() - today.getTime();
    //日付に変換
    myDay = Math.floor(myseq / (1000 * 60 * 60 * 24)) + 1;
    if (myDay < 0 || myDay > Icount) {
        $('#dispMessage p').html('指定された日付は過去または<br/>' + Icount + '日以上の未来日です。更新しますか？');
        return false;
    }
    else {
        return true;
    }
}

// 日付チェック 案件詳細(簡易)
function DateCheckKani() {
    //チェック用日数の取得
    Icount = document.getElementById("hidDate").value;
    //繰り返し区分の取得
    RepeatKbn = document.getElementById("hidRepeatKbn").value;
    var today = new Date();
    myseq = 0;
    if (RepeatKbn == "1") {
        // 当日登録の場合

        // 非表示になっておりrblRepeatKbnが見つけられない場合はreturn trueとする。
        if (typeof document.getElementsByName("ctl00$MainContent$rblRepeatKbn")[0] == 'undefined') {
            return true;
        }

        if (document.getElementsByName("ctl00$MainContent$rblRepeatKbn")[0].checked == true) {
            //画面の日付の取得
            txtData = document.getElementById("txtDate").value;
            var setDate = new Date(txtData);
            myseq = setDate.getTime() - today.getTime();
        }
        else {
            //画面の日付の取得
            txtFromRepeatSpan = document.getElementById("txtFromRepeatSpan").value;
            var setRepeatDate = new Date(txtFromRepeatSpan);
            myseq = setRepeatDate.getTime() - today.getTime();
        }
    }
    else {
        //画面の日付の取得
        txtData = document.getElementById("txtDate").value;
        var setDate = new Date(txtData);
        myseq = setDate.getTime() - today.getTime();
    }
    //日付に変換
    myDay = Math.floor(myseq / (1000 * 60 * 60 * 24)) + 1;
    if (myDay < 0 || myDay > Icount) {
        $('#dispMessage p').html('指定された日付は過去または<br/>' + Icount + '日以上の未来日です。更新しますか？');
        return false;
    }
    else {
        return true;
    }
}

// 店舗マスタメンテナンス(店舗削除時)
function confirmMessage() {
    // 有効な担当者枠の件数
    tantoCount = document.getElementById("hidTantoFrameCount").value;

    if (tantoCount > 0) {
        $('#dispMessage p').html('有効な枠が存在します。一度削除すると元に戻せません。削除してもよろしいですか？');
        return false;
    } else {
        return true;
    }
}

// ダイアログのデザイン(余計な装飾を取り除く)
function DialogPopUpDesign(footerId, dispMessageId) {
    // 画面全体
    $('.ui-widget-content').css('border', '0').css('background', 'white').css('color', 'black');
    // タイトルの背景色
    //$('.ui-dialog-title').css('background-color', 'white');
    // タイトルバー
    //$('.ui-dialog-titlebar').hide();
    // 内容
    $('.ui-dialog-content').css('background-color', 'white');
    // デフォルトフォントサイズ
    $('.ui-dialog-widget').css('font-size', '');
    // ボタン範囲
    $('.ui-dialog-buttonset').css('float', 'none');
    // ボタンパネル
    $('.ui-dialog-buttonpane').css('text-align', 'center');
    // ボタンサイズ・位置
    $('.ui-dialog-buttonset button').css('width', '120').css('margin', '10px');
    $('.ui-resizable-handle').css('display', 'none');
    // フッター
    //	footerId = "#" + footerId;
    //	$(footerId).appendTo('.ui-dialog');
    // メッセージ文字の装飾
    dispMessageId = "#" + dispMessageId + " p";
    $(dispMessageId).css({ 'font-weight': 'bold', 'font-size': '17px', 'text-align': 'center', 'padding-top': '20px' });
}
