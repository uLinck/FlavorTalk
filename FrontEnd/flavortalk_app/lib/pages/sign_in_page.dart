import 'package:flavortalk_app/features/auth/providers/sign_in_controller_provider.dart';
import 'package:flavortalk_app/routes.dart';
import 'package:flavortalk_app/widgets/buttons/flavor_action_button.dart';
import 'package:flavortalk_app/widgets/fields/flavor_field.dart';
import 'package:flavortalk_app/widgets/scaffolds/flavor_unauthorized_scaffold.dart';
import 'package:flavortalk_app/widgets/typography/flavor_heading.dart';
import 'package:flavortalk_app/widgets/typography/flavor_subheading.dart';
import 'package:flutter/material.dart';
import 'package:hooks_riverpod/hooks_riverpod.dart';

class SignInPage extends HookConsumerWidget {
  const SignInPage({super.key});

  @override
  Widget build(BuildContext context, WidgetRef ref) {
    final state = ref.watch(signInControllerProvider);
    final controller = ref.read(signInControllerProvider.notifier);

    return FlavorUnauthorizedScaffold(
      backRoute: Routes.root,
      topChild: Column(
        crossAxisAlignment: CrossAxisAlignment.start,
        spacing: 30,
        children: [
          const Column(
            crossAxisAlignment: CrossAxisAlignment.start,
            children: [
              FlavorHeading(text: 'Welcome Back'),
              FlavorSubheading(text: 'Enter your details below'),
            ],
          ),
          Column(
            children: [
              FlavorField(
                label: 'E-mail',
                hint: 'example@flavortalk.com',
                onChanged: controller.setEmail,
              ),
              FlavorField(
                label: 'Password',
                hint: 'Your Password',
                onChanged: controller.setPassword,
              ),
            ],
          ),
        ],
      ),
      bottomChild: Column(
        children: [
          if (state.errorMessage != null)
            Row(
              children: [
                Text(
                  state.errorMessage!,
                  style: const TextStyle(color: Colors.red),
                ),
              ],
            ),

          if (state.isLoading)
            const CircularProgressIndicator()
          else
            FlavorActionButton(
              text: 'Sign In',
              onPressed: () => controller.signIn(context),
            ),
        ],
      ),
    );
  }
}
