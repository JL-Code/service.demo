/**
 * Created by Liu on 2017/6/27.
 */
/**
 * @desc 日志输出
 * @param level 日志级别 1成功 2提示 3警告 4错误
 * @param msg 日志消息
 */
function outLog(level, msg) {
    var result = "";
    switch (level) {
        case 1:
            result = "<p><span class='text-green'>[成功]:</span> " + msg + "</p>";
            break;
        case 2:
            result = "<p><span class='text-blue'>[提示]:</span> " + msg + "</p>";
            break;
        case 3:
            result = "<p><span class='text-yellow'>[警告]:</span> " + msg + "</p>";
            break;
        case 4:
            result = "<p><span class='text-red'>[错误]:</span> " + msg + "</p>";
            break;
        default:
            break;
    }
    return result;
}