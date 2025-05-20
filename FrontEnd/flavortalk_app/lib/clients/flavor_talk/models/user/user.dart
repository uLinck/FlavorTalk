import 'dart:convert';

import 'package:freezed_annotation/freezed_annotation.dart';
import 'package:shared_preferences/shared_preferences.dart';

part 'user.freezed.dart';
part 'user.g.dart';

@freezed
abstract class User with _$User {
  const factory User({
    required String id,
    required String email,
    required String name,
  }) = _User;

  factory User.fromJson(Map<String, Object?> json) => _$UserFromJson(json);
}

extension UserPrefs on SharedPreferencesAsync {
  Future<User> getUser() async {
    final userJson = await getString('user') ?? '';
    final decoded = jsonDecode(userJson);
    return User.fromJson(decoded as Map<String, Object?>);
  }
}
