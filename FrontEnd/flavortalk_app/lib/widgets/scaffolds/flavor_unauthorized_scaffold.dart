import 'package:flavortalk_app/routes.dart';
import 'package:flutter/material.dart';
import 'package:go_router/go_router.dart';

class FlavorUnauthorizedScaffold extends StatelessWidget {
  const FlavorUnauthorizedScaffold({
    required this.topChild,
    required this.bottomChild,
    this.topChildCrossAxisAlignment = CrossAxisAlignment.start,
    this.backRoute,
    super.key,
  });

  final Widget topChild;
  final CrossAxisAlignment topChildCrossAxisAlignment;
  final Widget bottomChild;
  final String? backRoute;

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: Padding(
        padding: const EdgeInsets.symmetric(vertical: 48, horizontal: 32),
        child: Column(
          crossAxisAlignment: topChildCrossAxisAlignment,
          spacing: 32,
          children: [
            if (backRoute != null)
              BackButton(
                onPressed: () => context.go(Routes.root),
                style: ButtonStyle(
                  padding: WidgetStateProperty.all(EdgeInsets.zero),
                  tapTargetSize: MaterialTapTargetSize.shrinkWrap,
                  minimumSize: WidgetStateProperty.all(Size.zero),
                ),
              ),
            Expanded(
              child: Column(
                crossAxisAlignment: topChildCrossAxisAlignment,
                spacing: 16,
                children: [topChild],
              ),
            ),
            bottomChild,
          ],
        ),
      ),
    );
  }
}
