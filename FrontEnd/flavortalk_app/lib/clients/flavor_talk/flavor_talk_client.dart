import 'dart:io';

import 'package:dio/dio.dart';
import 'package:dio/io.dart';
import 'package:flavortalk_app/clients/flavor_talk/childClients/auth_client.dart';
import 'package:flavortalk_app/clients/flavor_talk/childClients/user_client.dart';
import 'package:flavortalk_app/extensions/dio_extensions.dart';
import 'package:flutter_dotenv/flutter_dotenv.dart';


class FlavorTalkClient {
  factory FlavorTalkClient() {
    final url = dotenv.env['FLAVOR_TALK_URL'];
    if (url == null) throw Exception('FLAVOR_TALK_URL not initialized');
    return FlavorTalkClient._internal(createDio(url));
  }

  FlavorTalkClient._internal(Dio dio)
      : auth = AuthClient(dio),
        user = UserClient(dio);

  final AuthClient auth;
  final UserClient user;

  static Dio createDio(String baseUrl) {
    final dio = Dio()
      ..options.baseUrl = baseUrl
      ..options.receiveDataWhenStatusError = true
      ..options.validateStatus = (_) => true;

    // allow self-signed certificate
    (dio.httpClientAdapter as IOHttpClientAdapter).createHttpClient =
      () => HttpClient()
        ..badCertificateCallback = (cert, host, port) => true;

    return dio.withCookies();
  }
}
