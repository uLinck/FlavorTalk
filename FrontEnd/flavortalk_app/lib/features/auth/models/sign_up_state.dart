import 'package:freezed_annotation/freezed_annotation.dart';

part 'sign_up_state.freezed.dart';

@freezed
abstract class SignUpState with _$SignUpState {
  const factory SignUpState({
    required String name,
    required String email,
    required String password,
    required bool isLoading,
    String? errorMessage,
  }) = _SignUpState;

  factory SignUpState.initial() =>
    const SignUpState(name: '', email: '', password: '', isLoading: false);
}
