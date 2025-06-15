import 'package:flavortalk_app/routes.dart';
import 'package:flavortalk_app/widgets/buttons/flavor_action_button.dart';
import 'package:flavortalk_app/widgets/scaffolds/flavor_unauthorized_scaffold.dart';
import 'package:flavortalk_app/widgets/typography/flavor_heading.dart';
import 'package:flavortalk_app/widgets/typography/flavor_subheading.dart';
import 'package:flutter/material.dart';
import 'package:go_router/go_router.dart';
import 'package:hooks_riverpod/hooks_riverpod.dart';

class RootPage extends HookConsumerWidget {
  const RootPage({super.key});

  @override
  Widget build(BuildContext context, WidgetRef ref) {
    return Container(
      alignment: Alignment.center,
      constraints: const BoxConstraints.expand(),
      color: Colors.white,
      child: FlavorUnauthorizedScaffold(
        topChildCrossAxisAlignment: CrossAxisAlignment.center,
        topChild: const Expanded(
          child: Column(
            mainAxisAlignment: MainAxisAlignment.center,
            children: [
              FlavorHeading(text: 'FlavorTalk'),
              FlavorSubheading(text: 'Discover. Rate. Savor'),
            ],
          ),
        ),
        bottomChild: Column(
          spacing: 8,
          children: [
            FlavorActionButton(
              text: 'Sign Up',
              onPressed: () => context.go(Routes.signUp),
              outlined: true,
            ),
            FlavorActionButton(
              text: 'Sign In',
              onPressed: () => context.go(Routes.signIn),
            ),
          ],
        ),
      ),
    );
  }
}
