import 'package:freezed_annotation/freezed_annotation.dart';

part 'sign_in_state.freezed.dart';

@freezed
abstract class SignInState with _$SignInState {
  const factory SignInState({
    required String email,
    required String password,
    required bool isLoading,
    String? errorMessage,
  }) = _SignInState;

  factory SignInState.initial() => const SignInState(
    email: '',
    password: '',
    isLoading: false,
  );
}
