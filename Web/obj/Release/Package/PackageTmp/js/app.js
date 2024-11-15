function Generator() { };

Generator.prototype.rand = Math.floor(Math.random() * 26) + Date.now();

Generator.prototype.GetId = function () {
    return this.rand++;
};
var CookieGeneretor = new Generator();

$(document).ready(function () {

    GetCurrentCurtList();

    $(".btn-cart").click(function () {
        // disable button
        $(".btn-cart").prop("disabled", true);
        // add spinner to button
        $(this).html(
          `<span class="spinner-border" role="status" aria-hidden="true"></span> <i class="fa fa-refresh fa-spin"></i> Processing`
        );

        AddThisItemToCartList(this);
    });

    $("#empty_cart_button").click(function () {
        ClearAllCartItem();
    });
});

function SearchFormValidation() {
    var searchText = $.trim($("#search").val());
    if (searchText == "" || searchText == "Search entire store here...") {
        alert("Please enter some search keyword");
        return false;
    }
}

function DoAutoCompleteAction() {
    $(".dummy_cp_btn_fire").click();
}


function UpdateCart() {
    var browserCookie = GetCookie("VisitorRandomId");
    $("#Dummy_Browser_CookieId").val(browserCookie);

}

function ClearAllCartItem() {
    var browserCookie = GetCookie("VisitorRandomId");

    $.post("/Common/ClearMyCartHistory", { userCookie: browserCookie }, function (data) {
        window.location.replace(data);
    });

}


function RemoveCurrentProductItem(productId) {
    var browserCookie = GetCookie("VisitorRandomId");
    $.get("/Common/jQueryRemoveCurrentCartItem", { userCookie: browserCookie, productId: productId }, function (data) {
        window.location.replace(data);
    });
}

function GetCurrentCurtList() {

    var browserCookie = GetCookie("VisitorRandomId");

    $.get("/Common/GetMyCartListItems", { userCookie: browserCookie }, function (data) {

        if (data) {
            if (data.length > 0) {
                var price = 0;
                for (i = 0; i < data.length; i++) {
                    if (i < 3) {
                        var itemText = "<li class='item'><div class='item-inner'>";
                        itemText = itemText + "<a class='product-image' title='" + data[i].Name + "'" + " href='#'><img alt=" + data[i].Name + " src=" + domainURL + "/" + data[i].ImageURL + "> </a>";
                        itemText = itemText + "<div class='product-details'><div class='access'>";
                        //itemText = itemText + "<a class='btn-remove1' title='Remove This Item' href='#'>Remove</a>";
                        itemText = itemText + "<a class='btn-edit' title='Edit item' href='#'><i class='icon-pencil'></i><span class='hidden'>Edit item</span></a>";
                        itemText = itemText + "</div><strong>" + data[i].TotalQTY + "</strong> x <span class='price'>$" + data[i].Price + "</span>";
                        itemText = itemText + "<p class='product-name'><a href='" + domainURL + "/product/details/" + data[i].Id + "'>" + data[i].Name + "</a> </p>";
                        itemText = itemText + "</div></div></li>";
                        $(".mini-products-list").append(itemText);
                    }

                    price += (data[i].Price * data[i].TotalQTY);
                }

                $(".dummy_total_Cart_hints").text(data.length + " Items/ $" + price);
                $(".dummy_Cart_totals").text(" $" + price.toFixed(2));
                $("#dummy_hidden_Cart_totals").val(price.toFixed(2));
                $(".dummy_PriceWithCost").text(" $" + parseFloat(price + 50).toFixed(2));
                $("#Dummy_hidden_TotalAmount_ForCart").val(parseFloat(price + 50).toFixed(2));
                $("#top-cart-content").removeClass("hidden");

                $(".dummy_top_Cart_ActionsLink").html("");
                if (browserCookie == "")
                    browserCookie = "registered";
                var topCartAction = "<a href=" + domainURL + "/shop/my-cart/" + browserCookie + " class='btn-checkout'><span>Checkout</span></a>";
                topCartAction = topCartAction + "<a href=" + domainURL + "/shop/my-cart/" + browserCookie + " class='view-cart'><span>View Cart</span></a>";
                $(".dummy_top_Cart_ActionsLink").append(topCartAction);

            } else {
                $("#top-cart-content").addClass("hidden");
            }
        } else {
            $("#top-cart-content").addClass("hidden");
        }
    });
}

function AddThisItemToCartList(elem) {

    var cok = document.cookie;
    if (appUserName == "" && cok == "") {
        setCookie("VisitorRandomId", CookieGeneretor.GetId(), 30);
    }

    var browserCookie = GetCookie("VisitorRandomId");
    var proId = $.trim($(elem).attr("pid"));
    var qty = $.trim($(".dummy_QTY_Value").val());
    if (qty == "")
        qty = 1;

    $.get("/Common/AddItemToMyCartList", { userCookie: browserCookie, productId: proId, QTY: qty }, function (data) {
        $(".mini-products-list li").remove();
        GetCurrentCurtList();
        setTimeout(function () {
            $(".btn-cart").prop("disabled", false);
            $(elem).html("Add to cart");
        }, 200);

    });



    //var browser = get_browser_info();
    //console.log(browser.name);
    //console.log(browser.version);
}



function GetDynamicCookieId() {
    return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
        var r = Math.random() * 16 | 0, v = c == 'x' ? r : (r & 0x3 | 0x8);
        return v.toString(16);
    });
}


function setCookie(cname, cvalue, exdays) {
    var d = new Date();
    d.setTime(d.getTime() + (exdays * 24 * 60 * 60 * 1000));
    var expires = "expires=" + d.toUTCString();
    document.cookie = cname + "=" + cvalue + ";" + expires + ";path=/";
}

function GetCookie(cname) {
    var name = cname + "=";
    var ca = document.cookie.split(';');
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) == ' ') {
            c = c.substring(1);
        }
        if (c.indexOf(name) == 0) {
            return c.substring(name.length, c.length);
        }
    }
    return "";
}

function checkCookie() {
    var user = getCookie("username");
    if (user != "") {
        alert("Welcome again " + user);
    } else {
        user = prompt("Please enter your name:", "");
        if (user != "" && user != null) {
            setCookie("username", user, 365);
        }
    }
}

function get_browser_info() {
    var ua = navigator.userAgent, tem, M = ua.match(/(opera|chrome|safari|firefox|msie|trident(?=\/))\/?\s*(\d+)/i) || [];
    if (/trident/i.test(M[1])) {
        tem = /\brv[ :]+(\d+)/g.exec(ua) || [];
        return { name: 'IE ', version: (tem[1] || '') };
    }
    if (M[1] === 'Chrome') {
        tem = ua.match(/\bOPR\/(\d+)/)
        if (tem != null) { return { name: 'Opera', version: tem[1] }; }
    }
    M = M[2] ? [M[1], M[2]] : [navigator.appName, navigator.appVersion, '-?'];
    if ((tem = ua.match(/version\/(\d+)/i)) != null) { M.splice(1, 1, tem[1]); }
    return {
        name: M[0],
        version: M[1]
    };
}