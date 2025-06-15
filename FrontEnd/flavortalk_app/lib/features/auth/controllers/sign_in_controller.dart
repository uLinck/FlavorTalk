import 'package:flavortalk_app/clients/flavor_talk/flavor_talk_client.dart';
import 'package:flavortalk_app/features/auth/models/sign_in_state.dart';
import 'package:flavortalk_app/routes.dart';
import 'package:flutter/widgets.dart';
import 'package:go_router/go_router.dart';
import 'package:hooks_riverpod/hooks_riverpod.dart';

class SignInController extends StateNotifier<SignInState> {
  SignInController(this.flavorTalkClient) : super(SignInState.initial());

  final FlavorTalkClient flavorTalkClient;

  void setEmail(String email) => state = state.copyWith(email: email);
  void setPassword(String password) =>
      state = state.copyWith(password: password);

  Future<void> signIn(BuildContext context) async {
    state = state.copyWith(isLoading: true);
    try {
      final user = await flavorTalkClient.auth.signInAsync(
        state.email,
        state.password,
      );

      if (user == null) {
        state = state.copyWith(
          errorMessage: 'Invalid email or password.',
          isLoading: false,
        );
        return;
      }

      state = state.copyWith(errorMessage: null, isLoading: false);
    } on Exception {
      state = state.copyWith(
        errorMessage: 'Something went wrong.',
        isLoading: false,
      );

      return;
    }

    if (context.mounted) context.go(Routes.home);
  }
}
