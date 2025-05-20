import 'package:freezed_annotation/freezed_annotation.dart';

part 'response.freezed.dart';
part 'response.g.dart';

@freezed
abstract class ApiResponse with _$Response {
  const factory ApiResponse({
    Map<String, Object?>? data,
    @Default([]) List<String> errors,
    @Default([]) List<String> reasons,
  }) = _Response;

  factory ApiResponse.fromJson( Map<String, Object?> json) =>
    _$ResponseFromJson(json);
}
