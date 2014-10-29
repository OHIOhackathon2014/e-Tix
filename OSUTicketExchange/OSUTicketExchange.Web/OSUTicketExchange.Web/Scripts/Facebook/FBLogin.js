$(document).ready(function () {
    $('.facebook-login').click(function () {
        var responsd;
        FB.login(function (response) {
            if (response.status === 'connected') {
                $('.facebook-login').hide();
                GetUserInfo(response);
                $('.loader').show();
            } else if (response.status === 'not_authorized') {
                $('.loader').hide();
                $('.facebook-login').show();
            } else {
                $('.loader').hide();
                $('.facebook-login').show();
            }
        }, { scope: 'public_profile, email' });

    });
})
function GetUserInfo(response) {
    FB.api('/me', function (response) {
        Login(response);
    });

}
function Login(token) {
    var datasource = {
        Username: token.name,
        Email: token.email
    };
    $.ajax({
        type: 'POST',
        url: "/Account/FacebookLogin",
        data: datasource,
        success: function () {
            window.location.replace('/');
        }
    }).done(function () {
 
    });
}