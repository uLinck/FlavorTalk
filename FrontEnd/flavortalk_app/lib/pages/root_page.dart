import 'package:flavortalk_app/routes.dart';
import 'package:flutter/material.dart';
import 'package:go_router/go_router.dart';
import 'package:google_fonts/google_fonts.dart';
import 'package:hooks_riverpod/hooks_riverpod.dart';

class RootPage extends HookConsumerWidget {
  const RootPage({super.key});

  @override
  Widget build(BuildContext context, WidgetRef ref) {
    return Container(
      alignment: Alignment.center,
      constraints: const BoxConstraints.expand(),
      color: Colors.white,
      child: Column(
        mainAxisAlignment: MainAxisAlignment.center,
        children: [
          Text(
            'FlavorTalk',
            style: GoogleFonts.poppins(
              decoration: TextDecoration.none,
              color: Colors.black,
            ),
          ),
          ElevatedButton(
            onPressed: () => context.go(Routes.signIn),
            child: const Text('Sign In'),
          ),
        ],
      ),
    );
  }
}
