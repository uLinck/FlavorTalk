import 'package:dio/dio.dart';
import 'package:flavortalk_app/clients/flavor_talk/flavor_talk_client.dart';
import 'package:flavortalk_app/clients/flavor_talk/models/auth/sign_up_request.dart';
import 'package:flavortalk_app/features/auth/models/sign_up_state.dart';
import 'package:flavortalk_app/routes.dart';
import 'package:flutter/material.dart';
import 'package:go_router/go_router.dart';
import 'package:hooks_riverpod/hooks_riverpod.dart';

class SignUpController extends StateNotifier<SignUpState> {
  SignUpController(this.flavorTalkClient) : super(SignUpState.initial());

  final FlavorTalkClient flavorTalkClient;

  void setName(String name) => state = state.copyWith(name: name);
  void setEmail(String email) => state = state.copyWith(email: email);
  void setPassword(String password) =>
      state = state.copyWith(password: password);

  Future<void> signUp(BuildContext context) async {
    state = state.copyWith(isLoading: true);
    try {
      await flavorTalkClient.auth.signUpAsync(
        SignUpRequest(
          name: state.name,
          email: state.email,
          password: state.password,
        ),
      );

      if (context.mounted) context.go(Routes.home);
    } on DioException catch (e) {
      state = state.copyWith(
        errorMessage: 'Something went wrong.',
        isLoading: false,
      );
      print(e);
    }
  }
}
