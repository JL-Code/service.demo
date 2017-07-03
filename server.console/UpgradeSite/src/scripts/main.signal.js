/**
 * Created by Liu on 2017/6/27.
 */
+function ($) {
    swal.setDefaults({
        confirmButtonText: "确定",
        cancelButtonText: "取消"
    });
    //设置hubs的服务端连接地址
    //获取signalr连接对象
    var connection = $.hubConnection(upgrade_service_address);
    //创建客户端代理
    var Proxy = connection.createHubProxy("serviceMonitorHub");
    //启动signalr连接
    connection.start().done(function (response) {
        $('#message').append(outLog(1, 'signalr连接成功'));
        //开始网站升级
    }).fail(function (response) {
        $('#message').append(outLog(4, 'signalr连接失败 ' + response));
    });

    //添加客户端方法
    Proxy.on('print', function (message) {
        $('#message').append(message)
    });
    Proxy.on('notice', function (response) {
        $('#message').append(outLog(response.level, response.msg));
    });


}(jQuery);