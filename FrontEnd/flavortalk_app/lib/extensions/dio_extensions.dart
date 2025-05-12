import 'package:cookie_jar/cookie_jar.dart';
import 'package:dio/dio.dart';
import 'package:dio_cookie_manager/dio_cookie_manager.dart';
import 'package:flavortalk_app/clients/flavor_talk/models/response.dart';

extension DioExtensions on Dio {
  Dio withCookies() {
    final cookieJar = CookieJar();
    interceptors.add(CookieManager(cookieJar));
    return this;
  }
}

extension ResponseExtensions on Response<ApiResponse> {
  bool get isSuccess =>
    statusCode == 200 && apiRes != null;

  ApiResponse? get apiRes => data;
  Map<String, Object?>? get apiData => data?.data;
}
