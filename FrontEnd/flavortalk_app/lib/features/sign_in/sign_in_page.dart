import 'package:flavortalk_app/features/sign_in/providers/sign_in_controller_provider.dart';
import 'package:flutter/material.dart';
import 'package:flutter_hooks/flutter_hooks.dart';
import 'package:google_fonts/google_fonts.dart';
import 'package:hooks_riverpod/hooks_riverpod.dart';

class SignInPage extends HookConsumerWidget {
  const SignInPage({super.key});

  @override
  Widget build(BuildContext context, WidgetRef ref) {
    final state = ref.watch(signInControllerProvider);
    final controller = ref.read(signInControllerProvider.notifier);

    final emailFocus = useFocusNode();
    final passwordFocus = useFocusNode();

    return Scaffold(
      body: Padding(
        padding: const EdgeInsets.symmetric(vertical: 48, horizontal: 32),
        child: Column(
          mainAxisAlignment: MainAxisAlignment.center,
          children: [
            Expanded(
              child: Column(
                spacing: 30,
                children: [
                  Column(
                    children: [
                      Text(
                        'Welcome Back',
                        style: GoogleFonts.poppins(
                          decoration: TextDecoration.none,
                          color: Colors.black,
                          fontSize: 35,
                          fontWeight: FontWeight.w600,
                        ),
                      ),
                      Text(
                        'Enter your details below',
                        style: GoogleFonts.poppins(
                          decoration: TextDecoration.none,
                          color: Colors.black,
                          fontSize: 15,
                          fontWeight: FontWeight.w400,
                        ),
                      ),
                    ],
                  ),
                  Column(
                    children: [
                      TextField(
                        focusNode: emailFocus,
                        onChanged: controller.setEmail,
                        decoration: const InputDecoration(
                          labelText: 'E-mail',
                          border: OutlineInputBorder(),
                        ),
                      ),
                      TextField(
                        focusNode: passwordFocus,
                        onChanged: controller.setPassword,
                        decoration: const InputDecoration(
                          labelText: 'Password',
                          border: OutlineInputBorder(),
                        ),
                      ),
                    ],
                  ),
                ],
              ),
            ),
            const SizedBox(height: 20),
            if (state.isLoading)
              const CircularProgressIndicator()
            else
              ElevatedButton(
                onPressed: () => controller.signIn(context),
                child: const Text('Sign In'),
              ),
          ],
        ),
      ),
    );
  }
}
