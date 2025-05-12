// dart format width=80
// coverage:ignore-file
// GENERATED CODE - DO NOT MODIFY BY HAND
// ignore_for_file: type=lint
// ignore_for_file: unused_element, deprecated_member_use, deprecated_member_use_from_same_package, use_function_type_syntax_for_parameters, unnecessary_const, avoid_init_to_null, invalid_override_different_default_values_named, prefer_expression_function_bodies, annotate_overrides, invalid_annotation_target, unnecessary_question_mark

part of 'sign_in_state.dart';

// **************************************************************************
// FreezedGenerator
// **************************************************************************

// dart format off
T _$identity<T>(T value) => value;
/// @nodoc
mixin _$SignInState {

 String get email; String get password; bool get isLoading; String? get errorMessage;
/// Create a copy of SignInState
/// with the given fields replaced by the non-null parameter values.
@JsonKey(includeFromJson: false, includeToJson: false)
@pragma('vm:prefer-inline')
$SignInStateCopyWith<SignInState> get copyWith => _$SignInStateCopyWithImpl<SignInState>(this as SignInState, _$identity);



@override
bool operator ==(Object other) {
  return identical(this, other) || (other.runtimeType == runtimeType&&other is SignInState&&(identical(other.email, email) || other.email == email)&&(identical(other.password, password) || other.password == password)&&(identical(other.isLoading, isLoading) || other.isLoading == isLoading)&&(identical(other.errorMessage, errorMessage) || other.errorMessage == errorMessage));
}


@override
int get hashCode => Object.hash(runtimeType,email,password,isLoading,errorMessage);

@override
String toString() {
  return 'SignInState(email: $email, password: $password, isLoading: $isLoading, errorMessage: $errorMessage)';
}


}

/// @nodoc
abstract mixin class $SignInStateCopyWith<$Res>  {
  factory $SignInStateCopyWith(SignInState value, $Res Function(SignInState) _then) = _$SignInStateCopyWithImpl;
@useResult
$Res call({
 String email, String password, bool isLoading, String? errorMessage
});




}
/// @nodoc
class _$SignInStateCopyWithImpl<$Res>
    implements $SignInStateCopyWith<$Res> {
  _$SignInStateCopyWithImpl(this._self, this._then);

  final SignInState _self;
  final $Res Function(SignInState) _then;

/// Create a copy of SignInState
/// with the given fields replaced by the non-null parameter values.
@pragma('vm:prefer-inline') @override $Res call({Object? email = null,Object? password = null,Object? isLoading = null,Object? errorMessage = freezed,}) {
  return _then(_self.copyWith(
email: null == email ? _self.email : email // ignore: cast_nullable_to_non_nullable
as String,password: null == password ? _self.password : password // ignore: cast_nullable_to_non_nullable
as String,isLoading: null == isLoading ? _self.isLoading : isLoading // ignore: cast_nullable_to_non_nullable
as bool,errorMessage: freezed == errorMessage ? _self.errorMessage : errorMessage // ignore: cast_nullable_to_non_nullable
as String?,
  ));
}

}


/// @nodoc


class _SignInState implements SignInState {
  const _SignInState({required this.email, required this.password, required this.isLoading, this.errorMessage});
  

@override final  String email;
@override final  String password;
@override final  bool isLoading;
@override final  String? errorMessage;

/// Create a copy of SignInState
/// with the given fields replaced by the non-null parameter values.
@override @JsonKey(includeFromJson: false, includeToJson: false)
@pragma('vm:prefer-inline')
_$SignInStateCopyWith<_SignInState> get copyWith => __$SignInStateCopyWithImpl<_SignInState>(this, _$identity);



@override
bool operator ==(Object other) {
  return identical(this, other) || (other.runtimeType == runtimeType&&other is _SignInState&&(identical(other.email, email) || other.email == email)&&(identical(other.password, password) || other.password == password)&&(identical(other.isLoading, isLoading) || other.isLoading == isLoading)&&(identical(other.errorMessage, errorMessage) || other.errorMessage == errorMessage));
}


@override
int get hashCode => Object.hash(runtimeType,email,password,isLoading,errorMessage);

@override
String toString() {
  return 'SignInState(email: $email, password: $password, isLoading: $isLoading, errorMessage: $errorMessage)';
}


}

/// @nodoc
abstract mixin class _$SignInStateCopyWith<$Res> implements $SignInStateCopyWith<$Res> {
  factory _$SignInStateCopyWith(_SignInState value, $Res Function(_SignInState) _then) = __$SignInStateCopyWithImpl;
@override @useResult
$Res call({
 String email, String password, bool isLoading, String? errorMessage
});




}
/// @nodoc
class __$SignInStateCopyWithImpl<$Res>
    implements _$SignInStateCopyWith<$Res> {
  __$SignInStateCopyWithImpl(this._self, this._then);

  final _SignInState _self;
  final $Res Function(_SignInState) _then;

/// Create a copy of SignInState
/// with the given fields replaced by the non-null parameter values.
@override @pragma('vm:prefer-inline') $Res call({Object? email = null,Object? password = null,Object? isLoading = null,Object? errorMessage = freezed,}) {
  return _then(_SignInState(
email: null == email ? _self.email : email // ignore: cast_nullable_to_non_nullable
as String,password: null == password ? _self.password : password // ignore: cast_nullable_to_non_nullable
as String,isLoading: null == isLoading ? _self.isLoading : isLoading // ignore: cast_nullable_to_non_nullable
as bool,errorMessage: freezed == errorMessage ? _self.errorMessage : errorMessage // ignore: cast_nullable_to_non_nullable
as String?,
  ));
}


}

// dart format on
