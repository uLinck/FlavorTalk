
import 'package:dio/dio.dart';
import 'package:flavortalk_app/clients/flavor_talk/models/user/user.dart';

class UserClient{
  UserClient(Dio dio)
    : _dio = dio;

  final Dio _dio;

  Future<List<User>?> getAllUsers() async {
    final response = await _dio.get<List<User>>('/user');
    if (response.statusCode != 200 || response.data == null) return [];
    return response.data;
  }
}
