import 'dart:convert';

import 'package:dio/dio.dart';
import 'package:flavortalk_app/clients/flavor_talk/models/response.dart';
import 'package:flavortalk_app/clients/flavor_talk/models/user/user.dart';
import 'package:flavortalk_app/extensions/dio_extensions.dart';
import 'package:shared_preferences/shared_preferences.dart';

class AuthClient {
  AuthClient(Dio dio)
    : _dio = dio;

  final Dio _dio;
  final SharedPreferencesAsync _prefs = SharedPreferencesAsync();

  /// Sends a POST request to the sign-in endpoint with the provided
  /// email and password. Returns `true` if the response status code
  /// is 200, indicating a successful sign-in, otherwise returns `false`.
  Future<User?> signInAsync(String email, String password) async {
    final response = await _dio.post<ApiResponse>(
      '/auth/signin',
      options:  Options(contentType: Headers.jsonContentType),
      data: jsonEncode(<String, String>{
        'email': email,
        'password': password,
      })
    );

    if (!response.isSuccess) return null;

    final dataJson = jsonEncode(response.apiData);
    await _prefs.setString('user', dataJson);

    return User.fromJson(response.apiData!);
  }
}
