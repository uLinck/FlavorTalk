import 'dart:convert';

import 'package:dio/dio.dart';
import 'package:flavortalk_app/clients/flavor_talk/models/api_response.dart';
import 'package:flavortalk_app/clients/flavor_talk/models/auth/sign_in_request.dart';
import 'package:flavortalk_app/clients/flavor_talk/models/auth/sign_up_request.dart';
import 'package:flavortalk_app/clients/flavor_talk/models/user/user.dart';
import 'package:flavortalk_app/extensions/dio_extensions.dart';
import 'package:shared_preferences/shared_preferences.dart';

class AuthClient {
  AuthClient(Dio dio) : _dio = dio;

  final Dio _dio;
  final SharedPreferencesAsync _prefs = SharedPreferencesAsync();

  /// Sends a POST request to the sign-in endpoint with the provided
  /// email and password. Returns `true` if the response status code
  /// is 200, indicating a successful sign-in, otherwise returns `false`.
  Future<User?> signInAsync(SignInRequest req) async {
    final res = await _dio.post<Map<String, Object?>>(
      '/auth/signin',
      options: Options(contentType: Headers.jsonContentType),
      data: req.toJson(),
    );

    if (res.statusCode! >= 300) return null;

    final apiRes = ApiResponse.fromJson(res.data!);

    await _prefs.setString('user', jsonEncode(apiRes.data));

    return User.fromJson(apiRes.data!);
  }

  Future<User?> signUpAsync(SignUpRequest req) async {
    final res = await _dio.post<Map<String, Object?>>(
      '/auth/signup',
      options: Options(contentType: Headers.jsonContentType),
      data: req.toJson(),
    );

    if (res.statusCode! >= 300) return null;

    return signInAsync(SignInRequest(email: req.email, password: req.password));
  }
}
