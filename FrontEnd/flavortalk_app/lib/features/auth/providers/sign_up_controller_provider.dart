import 'package:flavortalk_app/clients/flavor_talk/flavor_talk_provider.dart';
import 'package:flavortalk_app/features/auth/controllers/sign_up_controller.dart';
import 'package:flavortalk_app/features/auth/models/sign_up_state.dart';
import 'package:hooks_riverpod/hooks_riverpod.dart';

final signUpControllerProvider =
  StateNotifierProvider<SignUpController, SignUpState>((ref) {
    final flavorTalkClient = ref.read(flavorTalkClientProvider);
    return SignUpController(flavorTalkClient);
  });
