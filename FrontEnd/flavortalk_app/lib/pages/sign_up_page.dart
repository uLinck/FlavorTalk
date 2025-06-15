import 'package:flavortalk_app/features/auth/providers/sign_up_controller_provider.dart';
import 'package:flavortalk_app/routes.dart';
import 'package:flavortalk_app/widgets/buttons/flavor_action_button.dart';
import 'package:flavortalk_app/widgets/fields/flavor_field.dart';
import 'package:flavortalk_app/widgets/scaffolds/flavor_unauthorized_scaffold.dart';
import 'package:flavortalk_app/widgets/typography/flavor_heading.dart';
import 'package:flavortalk_app/widgets/typography/flavor_subheading.dart';
import 'package:flutter/material.dart';
import 'package:hooks_riverpod/hooks_riverpod.dart';

class SignUpPage extends HookConsumerWidget {
  const SignUpPage({super.key});

  @override
  Widget build(BuildContext context, WidgetRef ref) {
    final state = ref.watch(signUpControllerProvider);
    final controller = ref.read(signUpControllerProvider.notifier);

    return FlavorUnauthorizedScaffold(
      backRoute: Routes.root,
      topChild: Column(
        crossAxisAlignment: CrossAxisAlignment.start,
        spacing: 30,
        children: [
          const Column(
            crossAxisAlignment: CrossAxisAlignment.start,
            children: [
              FlavorHeading(text: 'Create your first account'),
              FlavorSubheading(text: 'Welcome to our beloved app!'),
            ],
          ),
          Column(
            children: [
              FlavorField(
                label: 'Name',
                hint: 'Your Name',
                onChanged: controller.setName,
              ),
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
              text: 'Sign Up',
              onPressed: () => controller.signUp(context),
            ),
        ],
      ),
    );
  }
}
