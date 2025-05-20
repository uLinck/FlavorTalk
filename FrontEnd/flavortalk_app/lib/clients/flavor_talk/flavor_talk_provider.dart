import 'package:flavortalk_app/clients/flavor_talk/flavor_talk_client.dart';
import 'package:hooks_riverpod/hooks_riverpod.dart';

final flavorTalkClientProvider = Provider<FlavorTalkClient>((ref) {
  return FlavorTalkClient();
});
