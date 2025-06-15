import 'package:freezed_annotation/freezed_annotation.dart';

part 'api_response.freezed.dart';
part 'api_response.g.dart';

@freezed
abstract class ApiResponse with _$ApiResponse {
  const factory ApiResponse({
    Map<String, Object?>? data,
    @Default([]) List<String> errors,
    @Default([]) List<String> reasons,
  }) = _ApiResponse;

  factory ApiResponse.fromJson(Map<String, Object?> json) =>
    _$ApiResponseFromJson(json);
}
