// dart format width=80
// coverage:ignore-file
// GENERATED CODE - DO NOT MODIFY BY HAND
// ignore_for_file: type=lint
// ignore_for_file: unused_element, deprecated_member_use, deprecated_member_use_from_same_package, use_function_type_syntax_for_parameters, unnecessary_const, avoid_init_to_null, invalid_override_different_default_values_named, prefer_expression_function_bodies, annotate_overrides, invalid_annotation_target, unnecessary_question_mark

part of 'response.dart';

// **************************************************************************
// FreezedGenerator
// **************************************************************************

// dart format off
T _$identity<T>(T value) => value;

/// @nodoc
mixin _$Response {

 Map<String, Object?>? get data; List<String> get errors; List<String> get reasons;
/// Create a copy of Response
/// with the given fields replaced by the non-null parameter values.
@JsonKey(includeFromJson: false, includeToJson: false)
@pragma('vm:prefer-inline')
$ResponseCopyWith<ApiResponse> get copyWith => _$ResponseCopyWithImpl<ApiResponse>(this as ApiResponse, _$identity);

  /// Serializes this Response to a JSON map.
  Map<String, dynamic> toJson();


@override
bool operator ==(Object other) {
  return identical(this, other) || (other.runtimeType == runtimeType&&other is ApiResponse&&const DeepCollectionEquality().equals(other.data, data)&&const DeepCollectionEquality().equals(other.errors, errors)&&const DeepCollectionEquality().equals(other.reasons, reasons));
}

@JsonKey(includeFromJson: false, includeToJson: false)
@override
int get hashCode => Object.hash(runtimeType,const DeepCollectionEquality().hash(data),const DeepCollectionEquality().hash(errors),const DeepCollectionEquality().hash(reasons));

@override
String toString() {
  return 'Response(data: $data, errors: $errors, reasons: $reasons)';
}


}

/// @nodoc
abstract mixin class $ResponseCopyWith<$Res>  {
  factory $ResponseCopyWith(ApiResponse value, $Res Function(ApiResponse) _then) = _$ResponseCopyWithImpl;
@useResult
$Res call({
 Map<String, Object?>? data, List<String> errors, List<String> reasons
});




}
/// @nodoc
class _$ResponseCopyWithImpl<$Res>
    implements $ResponseCopyWith<$Res> {
  _$ResponseCopyWithImpl(this._self, this._then);

  final ApiResponse _self;
  final $Res Function(ApiResponse) _then;

/// Create a copy of Response
/// with the given fields replaced by the non-null parameter values.
@pragma('vm:prefer-inline') @override $Res call({Object? data = freezed,Object? errors = null,Object? reasons = null,}) {
  return _then(_self.copyWith(
data: freezed == data ? _self.data : data // ignore: cast_nullable_to_non_nullable
as Map<String, Object?>?,errors: null == errors ? _self.errors : errors // ignore: cast_nullable_to_non_nullable
as List<String>,reasons: null == reasons ? _self.reasons : reasons // ignore: cast_nullable_to_non_nullable
as List<String>,
  ));
}

}


/// @nodoc
@JsonSerializable()

class _Response implements ApiResponse {
  const _Response({final  Map<String, Object?>? data, final  List<String> errors = const [], final  List<String> reasons = const []}): _data = data,_errors = errors,_reasons = reasons;
  factory _Response.fromJson(Map<String, dynamic> json) => _$ResponseFromJson(json);

 final  Map<String, Object?>? _data;
@override Map<String, Object?>? get data {
  final value = _data;
  if (value == null) return null;
  if (_data is EqualUnmodifiableMapView) return _data;
  // ignore: implicit_dynamic_type
  return EqualUnmodifiableMapView(value);
}

 final  List<String> _errors;
@override@JsonKey() List<String> get errors {
  if (_errors is EqualUnmodifiableListView) return _errors;
  // ignore: implicit_dynamic_type
  return EqualUnmodifiableListView(_errors);
}

 final  List<String> _reasons;
@override@JsonKey() List<String> get reasons {
  if (_reasons is EqualUnmodifiableListView) return _reasons;
  // ignore: implicit_dynamic_type
  return EqualUnmodifiableListView(_reasons);
}


/// Create a copy of Response
/// with the given fields replaced by the non-null parameter values.
@override @JsonKey(includeFromJson: false, includeToJson: false)
@pragma('vm:prefer-inline')
_$ResponseCopyWith<_Response> get copyWith => __$ResponseCopyWithImpl<_Response>(this, _$identity);

@override
Map<String, dynamic> toJson() {
  return _$ResponseToJson(this, );
}

@override
bool operator ==(Object other) {
  return identical(this, other) || (other.runtimeType == runtimeType&&other is _Response&&const DeepCollectionEquality().equals(other._data, _data)&&const DeepCollectionEquality().equals(other._errors, _errors)&&const DeepCollectionEquality().equals(other._reasons, _reasons));
}

@JsonKey(includeFromJson: false, includeToJson: false)
@override
int get hashCode => Object.hash(runtimeType,const DeepCollectionEquality().hash(_data),const DeepCollectionEquality().hash(_errors),const DeepCollectionEquality().hash(_reasons));

@override
String toString() {
  return 'Response(data: $data, errors: $errors, reasons: $reasons)';
}


}

/// @nodoc
abstract mixin class _$ResponseCopyWith<$Res> implements $ResponseCopyWith<$Res> {
  factory _$ResponseCopyWith(_Response value, $Res Function(_Response) _then) = __$ResponseCopyWithImpl;
@override @useResult
$Res call({
 Map<String, Object?>? data, List<String> errors, List<String> reasons
});




}
/// @nodoc
class __$ResponseCopyWithImpl<$Res>
    implements _$ResponseCopyWith<$Res> {
  __$ResponseCopyWithImpl(this._self, this._then);

  final _Response _self;
  final $Res Function(_Response) _then;

/// Create a copy of Response
/// with the given fields replaced by the non-null parameter values.
@override @pragma('vm:prefer-inline') $Res call({Object? data = freezed,Object? errors = null,Object? reasons = null,}) {
  return _then(_Response(
data: freezed == data ? _self._data : data // ignore: cast_nullable_to_non_nullable
as Map<String, Object?>?,errors: null == errors ? _self._errors : errors // ignore: cast_nullable_to_non_nullable
as List<String>,reasons: null == reasons ? _self._reasons : reasons // ignore: cast_nullable_to_non_nullable
as List<String>,
  ));
}


}

// dart format on
