import 'package:flavortalk_app/clients/flavor_talk/flavor_talk_provider.dart';
import 'package:flavortalk_app/features/auth/controllers/sign_in_controller.dart';
import 'package:flavortalk_app/features/auth/models/sign_in_state.dart';
import 'package:hooks_riverpod/hooks_riverpod.dart';

final signInControllerProvider =
  StateNotifierProvider<SignInController, SignInState>((ref) {
    final flavorTalkClient = ref.read(flavorTalkClientProvider);
    return SignInController(flavorTalkClient);
  });
