// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'response.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

_Response _$ResponseFromJson(Map<String, dynamic> json) => _Response(
  data: json['data'] as Map<String, dynamic>?,
  errors:
      (json['errors'] as List<dynamic>?)?.map((e) => e as String).toList() ??
      const [],
  reasons:
      (json['reasons'] as List<dynamic>?)?.map((e) => e as String).toList() ??
      const [],
);

Map<String, dynamic> _$ResponseToJson(_Response instance) => <String, dynamic>{
  'data': instance.data,
  'errors': instance.errors,
  'reasons': instance.reasons,
};
